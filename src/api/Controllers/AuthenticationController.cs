using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs.Authentication;

namespace TournamentManagementSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _svc;

        public AuthenticationController(IAuthenticationService svc)
        {
            _svc = svc;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResultDTO>> Register(AuthRegisterDTO authRegisterDTO)
        {
            var auth = await _svc.RegisterAsync(authRegisterDTO);
            return Ok(auth);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResultDTO>> Login(AuthLoginDTO authLoginDTO)
        {
            var auth = await _svc.LoginAsync(authLoginDTO);
            return Ok(auth);
        }

    }
}
