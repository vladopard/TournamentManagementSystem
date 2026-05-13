using TournamentManagementSystem.DTOs.Parameters;
using TournamentManagementSystem.DTOs.Player;
using TournamentManagementSystem.Helpers;

namespace TournamentManagementSystem.BusinessServices.BusinessInterfaces
{
    public interface IPlayerService
    {
        Task<PagedList<PlayerDTO>> GetAllPlayersPagedAsync(PlayerParameters playerParameters);
        Task<IEnumerable<PlayerDTO>> GetAllPlayersAsync();
        Task<PlayerDTO> GetPlayerAsync(int id);
        Task<PlayerDTO> AddPlayerAsync(PlayerCreateDTO playerCreateDTO);
        Task UpdatePlayerAsync(PlayerUpdateDTO playerUpdateDTO, int id);
        Task PatchPlayerAsync(PlayerPatchDTO patchedDTO, int id);
        Task DeletePlayerAsync(int id);
    }
}