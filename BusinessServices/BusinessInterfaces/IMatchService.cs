using TournamentManagementSystem.DTOs.Match;
using TournamentManagementSystem.DTOs.Parameters;
using TournamentManagementSystem.Helpers;

namespace TournamentManagementSystem.BusinessServices.BusinessInterfaces
{
    public interface IMatchService
    {
        Task<PagedList<MatchDTO>> GetAllMatchesPagedAsync(MatchParameters matchParameters);
        Task<IEnumerable<MatchDTO>> GetMatchesAsync();
        Task<MatchDTO> GetMatchAsync(int id);
        Task<MatchDTO> AddMatchAsync(MatchCreateDTO matchCreateDTO);
        Task UpdateMatchAsync(MatchUpdateDTO matchUpdateDTO, int id);
        Task PatchMatchAsync(MatchPatchDTO matchPatchedDTO, int id);
        Task DeleteMatchAsync(int id);
    }
}