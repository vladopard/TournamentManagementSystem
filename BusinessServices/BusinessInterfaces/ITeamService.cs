using System.Threading.Tasks;
using TournamentManagementSystem.DTOs.Team;

namespace TournamentManagementSystem.BusinessServices
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();
        Task<TeamDTO> GetTeamAsync(int id);
    }
}