using TournamentManagementSystem.DTOs.Player;

namespace TournamentManagementSystem.BusinessServices.BusinessInterfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDTO>> GetAllPlayersAsync();
        Task<PlayerDTO> GetPlayerAsync(int id);
        Task<PlayerDTO> AddPlayerAsync(PlayerCreateDTO playerCreateDTO);
        Task UpdatePlayerAsync(PlayerUpdateDTO playerUpdateDTO, int id);
        Task PatchPlayerAsync(PlayerPatchDTO patchedDTO, int id);
        Task DeletePlayerAsync(int id);
    }
}