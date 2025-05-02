using System.Threading.Tasks;
using TournamentManagementSystem.DTOs.Player;
using TournamentManagementSystem.DTOs.Team;

namespace TournamentManagementSystem.BusinessServices
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();
        Task<TeamDTO> GetTeamAsync(int id);
        Task<TeamDTO> AddTeamAsync(TeamCreateDTO teamCreateDTO);
        Task UpdateTeamAsync(TeamUpdateDTO teamUpdateDTO, int id);
        Task PatchTeamAsync(TeamPatchDTO patchedDTO, int id);
        Task DeleteTeamAsync(int id);
    }
}