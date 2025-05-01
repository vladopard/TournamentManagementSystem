using TournamentManagementSystem.DTOs;


namespace TournamentManagementSystem.BusinessServices.BusinessInterfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync();
        Task<TournamentDTO> GetSingleTournamentAsync(int id);
        Task<TournamentDTO> CreateTournamentAsync(
            TournamentCreateDTO tournamentCreateDTO);
        Task UpdateTournamentAsync(
            TournamentUpdateDTO tournamentUpdateDTO, int id);
        Task PatchTournamentAsync(
            TournamentPatchDTO patchedDTO, int id);
        Task DeleteTournamentAsync(int id);
    }
}