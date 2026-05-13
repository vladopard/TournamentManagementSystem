using TournamentManagementSystem.DTOs.PlayerStats;

namespace TournamentManagementSystem.BusinessServices.BusinessInterfaces
{
    public interface IPlayerMatchStatsService
    {
        Task<IEnumerable<PlayerMatchStatsDTO>> GetAllStatsForPlayerAsync(int playerId);
        Task<PlayerMatchStatsDTO> GetStatsForPlayerFromOneMatchAsync(int playerId, int matchId);
    }
}