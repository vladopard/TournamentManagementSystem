using TournamentManagementSystem.DTOs.Match;

namespace TournamentManagementSystem.BusinessServices.BusinessInterfaces
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchDTO>> GetMatchesAsync();
        Task<MatchDTO> GetMatchAsync(int id);
        Task<MatchDTO> AddMatchAsync(MatchCreateDTO matchCreateDTO);
        Task UpdateMatchAsync(MatchUpdateDTO matchUpdateDTO, int id);
        Task PatchMatchAsync(MatchPatchDTO matchPatchedDTO, int id);
        Task DeleteMatchAsync(int id);
    }
}