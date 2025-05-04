using System.Xml.Linq;
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
            var nameUpper = name.Trim().ToUpper();
            var locationUpper = location.Trim().ToUpper();
            var sportUpper = sportType.Trim().ToUpper();

            return await _context.Tournaments
                .AnyAsync(t =>
                    t.StartDate == start &&
                    t.EndDate == end &&
                    t.Name.ToUpper() == nameUpper &&
                    t.Location.ToUpper() == locationUpper &&
                    t.SportType.ToUpper() == sportUpper &&
                    // when updating, ignore the record being updated
                    (!excludedTournamentId.HasValue || t.TournamentId != excludedTournamentId.Value)
                );
        }

        public async Task<bool> TournamentFKExistsAsync(int tournamentId)
        {
            return await _context.Tournaments
                .AnyAsync(t => t.TournamentId == tournamentId);
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
            var nameUpper = name.Trim().ToUpper();

            return await _context.Organizers
                .AnyAsync(o => o.Name.ToUpper() == nameUpper && 
                (!excludedOrganizerId.HasValue || o.OrganizerId != excludedOrganizerId));
        }

        public async Task<bool> OrganizerContactInfoExistsAsync(string contactInfo, int? excludedOrganizerId = null)
        {
            var contactUpper = contactInfo.Trim().ToUpper();

            return await _context.Organizers
                .AnyAsync(o => o.ContactInfo.ToUpper() == contactUpper && 
                (!excludedOrganizerId.HasValue || o.OrganizerId != excludedOrganizerId));
        }

        //TEAM TEAM TEAM TEAM TEAM TEAM TEAM TEAM
        //TEAM TEAM TEAM TEAM TEAM TEAM TEAM TEAM
        //TEAM TEAM TEAM TEAM TEAM TEAM TEAM TEAM
        //TEAM TEAM TEAM TEAM TEAM TEAM TEAM TEAM

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams
                .AsNoTracking()
                .Include(t => t.Tournament)
                .Include(t => t.Players)
                .ToListAsync();
        }

        public async Task<Team?> GetTeamAsync(int id)
        {
            return await _context.Teams
                .Include(t => t.Tournament)
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task AddTeamAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            await _context.Entry(team)
                .Reference(t => t.Tournament)
                .LoadAsync();
        }

        public async Task DeleteTeamAsync(Team team)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeamAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TeamExistsAsync(string name, int tournamentId, int? excludeId = null)
        {
            string nameUpper = name.Trim().ToUpper();

            return await _context.Teams
                .AnyAsync(t =>
                t.Name.ToUpper() == nameUpper &&
                t.TournamentId == tournamentId &&
                (!excludeId.HasValue || t.TeamId != excludeId.Value));
        }

        //fordeletion
        public async Task<bool> TeamHasPlayersAsync(int teamId)
            => await _context.Players.AnyAsync(p => p.TeamId == teamId);

        public async Task<bool> TeamHasMatchesAsync(int teamId)
            => await _context.Matches.AnyAsync(m => m.HomeTeamId == teamId 
                                                || m.AwayTeamId == teamId);
        //PLAYERS PLAYERS PLAYERS PLAYERS //PLAYERS
        //PLAYERS PLAYERS PLAYERS PLAYERS //PLAYERS
        //PLAYERS PLAYERS PLAYERS PLAYERS //PLAYERS
        //PLAYERS PLAYERS PLAYERS PLAYERS //PLAYERS

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
                      => await _context.Players
                        .AsNoTracking()
                        .Include(p => p.Team)
                        .ToListAsync();

        public async Task<Player?> GetPlayerAsync(int id)
                      => await _context.Players
                        .Include(p => p.Team)
                        .FirstOrDefaultAsync(p => p.PlayerId == id);

        public async Task AddPlayerAsync(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            await _context.Entry(player)
                .Reference(p => p.Team)
                .LoadAsync();
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(Player player)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
        //HELPERI HELPERI
        public async Task<bool> PlayerExistsAsync(string firstName, string lastName,
            DateTime dob, int? excludeId = null)
        {
            var firstUpper = firstName.Trim().ToUpper();
            var lastUpper = lastName.Trim().ToUpper();

            return await _context.Players.AnyAsync(p =>
                p.FirstName.ToUpper() == firstUpper &&
                p.LastName.ToUpper() == lastUpper &&
                p.DateOfBirth == dob &&
                (!excludeId.HasValue || p.PlayerId != excludeId.Value));
        } 

        public async Task<bool> TeamFkExistsAsync(int teamId)
            => await _context.Teams.AnyAsync(t => t.TeamId == teamId);

        //MATCH MATCH MATCH MATCH MATCH MATCH MATCH MATCH MATCH
        //MATCH MATCH MATCH MATCH MATCH MATCH MATCH MATCH MATCH
        //MATCH MATCH MATCH MATCH MATCH MATCH MATCH MATCH MATCH
        //MATCH MATCH MATCH MATCH MATCH MATCH MATCH MATCH MATCH

        public async Task<IEnumerable<Match>> GetAllMatchesAsync()
            => await _context.Matches
                .AsNoTracking()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Tournament)
                .ToListAsync();
        public async Task<Match?> GetMatchAsync(int id)
            => await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Tournament)
                .FirstOrDefaultAsync(m => m.MatchId == id);
        public async Task AddMatchAsync(Match match)
        {
            await _context.AddAsync(match);
            await _context.SaveChangesAsync();
            await _context.Entry(match)
                .Reference(t => t.HomeTeam)
                .LoadAsync();
            await _context.Entry(match)
                .Reference(t => t.AwayTeam)
                .LoadAsync();
        }
        public async Task UpdateMatchAsync(Match match)
        {
            _context.Update(match);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMatchAsync(Match match)
        {
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
        }
        //HELPERI DRUGA DVA VEC POSTOJE za tournament i teamfk
        public async Task<bool> MatchExistsAsync(DateTime start, DateTime end,
            int homeTeamId, int awayTeamId, int? excludeId = null)
        {
            return await _context.Matches.AnyAsync(m =>
                m.StartDate == start &&
                m.EndDate == end &&
                m.HomeTeamId == homeTeamId &&
                m.AwayTeamId == awayTeamId &&
                (!excludeId.HasValue || excludeId != m.MatchId));
        }

        public async Task<bool> IsTeamBusyAsync(int teamId, 
            DateTime start, DateTime end, int? excludeMatchId)
        {

            return await _context.Matches.AnyAsync(m =>
                (m.HomeTeamId == teamId || m.AwayTeamId == teamId) &&
                start < m.EndDate && end > m.StartDate &&
                (!excludeMatchId.HasValue || m.MatchId != excludeMatchId.Value)
            );
        }

    }
}
