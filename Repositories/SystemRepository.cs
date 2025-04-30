using Microsoft.EntityFrameworkCore;
using TournamentManagementSystem.DbContexts;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private readonly TournamentDbContext _context;

        public SystemRepository(TournamentDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
        {
            return await _context.Tournaments
                .AsNoTracking()
                .Include(t => t.Organizer)
                .Include(t => t.Teams)
                .ToListAsync();
        }

        public async Task<Tournament?> GetTournamentAsync(int id)
            =>   await _context.Tournaments
                .AsNoTracking()
                .Include(t => t.Organizer)
                .Include(t => t.Teams)
                .FirstOrDefaultAsync(t => t.TournamentId == id);
        //posto samo vracamo iz baze nije potrebno da ef core trackuje ovo

        public async Task AddTournamentAsync(Tournament tournament)
        {
            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();

            // Explicitly load the Organizer after saving
            // This forces EF to load the Organizer object.
            // Why? Because you only set OrganizerId before.
            await _context.Entry(tournament)
                .Reference(t => t.Organizer)
                .LoadAsync();
        }
        public async Task UpdateTournamentAsync(Tournament tournament)
        {
            _context.Tournaments.Update(tournament);
            await SaveChangesAsync();
        }
        public async Task DeleteTournamentAsync(Tournament tournament)
        {
            var teamIds = await _context.Teams
                                .Where(t => t.TournamentId == tournament.TournamentId)
                                .Select(t => t.TeamId)
                                .ToListAsync();

            // Get IDs of Matches associated with the tournament
            var matchIds = await _context.Matches
                                         .Where(m => m.TournamentId == tournament.TournamentId)
                                         .Select(m => m.MatchId)
                                         .ToListAsync();

            // 2. Mark dependent entities for deletion in the correct order (reverse of dependency)

            
            // Delete PlayerMatchStats associated with the tournament's matches
            var relatedStats = _context.Results.Where(pms => matchIds.Contains(pms.MatchId));
            _context.Results.RemoveRange(relatedStats);

            // Delete Matches associated with the tournament
            // (Need to do this before Teams because Match -> Team is Restrict)
            var relatedMatches = _context.Matches.Where(m => m.TournamentId == tournament.TournamentId);
            _context.Matches.RemoveRange(relatedMatches);

            // Delete Players associated with the tournament's teams
            // (Need to do this before Teams because Team -> Player is Restrict)
            var relatedPlayers = _context.Players.Where(p => teamIds.Contains(p.TeamId)); // Assuming Player has TeamId FK
            _context.Players.RemoveRange(relatedPlayers);

            // Delete Teams associated with the tournament
            var relatedTeams = _context.Teams.Where(t => t.TournamentId == tournament.TournamentId);
            _context.Teams.RemoveRange(relatedTeams);

            // 3. Mark the Tournament itself for deletion
            _context.Tournaments.Remove(tournament); // Use the original or the loaded 'tournamentToDelete'

            // 4. Save all changes in a single transaction
            await _context.SaveChangesAsync();
        }
        public async Task<bool> TournamentExistsAsync(DateTime start,DateTime end,string name,
            string location,string sportType,int? excludedTournamentId = null)
        {
            return await _context.Tournaments
                .AnyAsync(t =>
                    t.StartDate == start &&
                    t.EndDate == end &&
                    t.Name == name &&
                    t.Location == location &&
                    t.SportType == sportType &&
                    // when updating, ignore the record being updated
                    (!excludedTournamentId.HasValue || t.TournamentId != excludedTournamentId.Value)
                );
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        //ORGANIZER ORGANIZER ORGANIZER ORGANIZER
        //ORGANIZER ORGANIZER ORGANIZER ORGANIZER
        //ORGANIZER ORGANIZER ORGANIZER ORGANIZER
        //ORGANIZER ORGANIZER ORGANIZER ORGANIZER

        public async Task<IEnumerable<Organizer>> GetAllOrganizersAsync()
        {
            return await _context.Organizers
                .AsNoTracking()
                .Include(o => o.Tournaments)
                .ToListAsync();
        }
        public async Task<Organizer?> GetOrganizerAsync(int id)
        {
            return await _context.Organizers
                .AsNoTracking()
                .Include(o => o.Tournaments)
                .FirstOrDefaultAsync(o => o.OrganizerId == id);
        }

        public async Task UpdateOrganizerAsync(Organizer organizer)
        {
            _context.Organizers.Update(organizer);
            await SaveChangesAsync();
        }

        public async Task AddOrganizerAsync(Organizer organizer)
        {
            await _context.Organizers.AddAsync(organizer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrganizerAsync(Organizer organizer)
        {
            _context.Organizers.Remove(organizer);
            await SaveChangesAsync();
        }

        public async Task<bool> OrganizerHasTournamentsAsync(int organizerId)
        {
            return await _context.Tournaments.AnyAsync(t => t.OrganizerId == organizerId);
        }

        //If no excludedOrganizerId was given → skip this check (always true).
        //If an excludedOrganizerId was given → make sure we ignore that specific organizer
        public async Task<bool> OrganizerExistsAsync(int organizerId)
        {
            return await _context.Organizers
                .AnyAsync(o => o.OrganizerId == organizerId);
        }
        public async Task<bool> OrganizerNameExistsAsync(string name, int? excludedOrganizerId = null)
        {
            return await _context.Organizers
                .AnyAsync(o => o.Name == name && 
                (!excludedOrganizerId.HasValue || o.OrganizerId != excludedOrganizerId));
        }

        public async Task<bool> OrganizerContactInfoExistsAsync(string contactInfo, int? excludedOrganizerId = null)
        {
            return await _context.Organizers
                .AnyAsync(o => o.ContactInfo == contactInfo && 
                (!excludedOrganizerId.HasValue || o.OrganizerId != excludedOrganizerId));
        }
    }
}
