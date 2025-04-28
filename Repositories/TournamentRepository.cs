using Microsoft.EntityFrameworkCore;
using TournamentManagementSystem.DbContexts;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentDbContext _context;

        public TournamentRepository(TournamentDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
        {
            return await _context.Tournaments
                .Include(t => t.Organizer)
                .Include(t => t.Teams)
                .ToListAsync();
        }

        public async Task<Tournament?> GetTournament(int id)
        {
            var tournament = await _context
                .Tournaments
                .Include(t => t.Organizer)
                .Include(t => t.Teams)
                .FirstOrDefaultAsync(t => t.TournamentId == id);
            return tournament;
        }

        public async Task<Tournament?> AddTournamentAsync(Tournament tournament)
        {
            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();

            // Explicitly load the Organizer after saving
            await _context.Entry(tournament)
                .Reference(t => t.Organizer)
                .LoadAsync();

            return tournament;
        }

        public async Task<bool> DeleteTournamentAsync(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null) return false;

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }



    }
}
