using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs.Authentication;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Helpers;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthRepository _repo;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly JwtSettings _jwt;

        public AuthenticationService(IAuthRepository repo, IOptions<JwtSettings> jwtOptions,
            UserManager<ApplicationUser> userMgr)
        {
            _repo = repo;
            _jwt = jwtOptions.Value;
            _userMgr = userMgr;
        }

        public async Task<AuthResultDTO> RegisterAsync(AuthRegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                DateJoined = DateTime.UtcNow
            };

            var result = await _repo.CreateUserAsync(user, registerDTO.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(
                    string.Join(";", result.Errors.Select(e => e.Description)));

            await _userMgr.AddToRoleAsync(user, "Player");

            var roles = await _userMgr.GetRolesAsync(user);
            return GenerateToken(user.Email!, roles);
        }

        public async Task<AuthResultDTO> LoginAsync(AuthLoginDTO loginDTO)
        {
            var user = await _repo.GetByEmailAsync(loginDTO.Email)
                ?? throw new KeyNotFoundException("Invalid credentials.");

            if (!await _repo.CheckPasswordAsync(user, loginDTO.Password))
                throw new InvalidOperationException("Invalid credentials");

            var roles = await _userMgr.GetRolesAsync(user);
            return GenerateToken(user.Email!, roles);
        }

        private AuthResultDTO GenerateToken(string email, IList<string> roles)
        {
            var key = _jwt.Key;
            var issuer = _jwt.Issuer;
            var audience = _jwt.Audience;
            var expires = DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new AuthResultDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expires
            };
        }
    }
}
