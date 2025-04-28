using TournamentManagementSystem.DTOs;
using TournamentManagementSystem.DTOs.Tournament;

namespace TournamentManagementSystem.BusinessServices.BusinessInterfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync();
        Task<TournamentDTO?> GetSingleTournamentAsync(int id);
        Task<TournamentDTO?> CreateTournamentAsync(
            TournamentCreateDTO tournamentCreateDTO);
        Task<bool> UpdateTournamentAsync(
            TournamentUpdateDTO tournamentUpdateDTO, int id);
        Task<bool> PatchTournamentAsync(
            TournamentPatchDTO patchedDTO, int id);
        Task<bool> DeleteTournamentAsync(int id);
    }
}