using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Repositories
{
    public interface ITournamentRepository
    {
        Task<Tournament?> AddTournamentAsync(Tournament tournament);
        Task<bool> DeleteTournamentAsync(int id);
        Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
        Task<Tournament?> GetTournament(int id);
        Task<bool> SaveChangesAsync();
    }
}