using Microsoft.AspNetCore.Identity;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Repositories
{
    //wrapper around UserManager
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
            => _userManager.CreateAsync(user, password);

        public Task<ApplicationUser?> GetByEmailAsync(string email)
            => _userManager.FindByEmailAsync(email);

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
            => _userManager.CheckPasswordAsync(user, password);

    }
}
