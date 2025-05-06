using TournamentManagementSystem.DTOs.Authentication;

namespace TournamentManagementSystem.BusinessServices.BusinessInterfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResultDTO> LoginAsync(AuthLoginDTO loginDTO);
        Task<AuthResultDTO> RegisterAsync(AuthRegisterDTO registerDTO);
    }
}