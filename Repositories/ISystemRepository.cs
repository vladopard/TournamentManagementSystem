using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Repositories
{
    public interface ISystemRepository
    {
        Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
        Task<Tournament?> GetTournamentAsync(int id);
        Task AddTournamentAsync(Tournament tournament);
        Task DeleteTournamentAsync(Tournament tournament);       
        Task UpdateTournamentAsync(Tournament tournament);
        Task<bool> TournamentExistsAsync(DateTime start,DateTime end,string name,
            string location,string sportType,int? excludedTournamentId = null);
        Task<bool> TournamentFKExistsAsync(int tournamentId);
        Task<bool> TournamentHasPlayersAsync(int tournamentId);
        Task<bool> SaveChangesAsync();
        //ORGANIZER METHODS
        Task<IEnumerable<Organizer>> GetAllOrganizersAsync();
        Task<Organizer?> GetOrganizerAsync(int id);
        Task AddOrganizerAsync(Organizer organizer);
        Task DeleteOrganizerAsync(Organizer organizer);
        Task UpdateOrganizerAsync(Organizer organizer);
        Task<bool> OrganizerHasTournamentsAsync(int organizerId);
        Task<bool> OrganizerExistsAsync(int organizerId);
        Task<bool> OrganizerContactInfoExistsAsync(string contactInfo, int? excludedOrganizerId = null);
        Task<bool> OrganizerNameExistsAsync(string name, int? excludedOrganizerId = null);
        //TEAM METHODS
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team?> GetTeamAsync(int id);
        Task AddTeamAsync(Team team);
        Task DeleteTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task<bool> TeamExistsAsync(string name, int tournamentId, int? excludeId = null);
        Task<bool> TeamHasPlayersAsync(int teamId);
        Task<bool> TeamHasMatchesAsync(int teamId);
        //PLAYERS
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player?> GetPlayerAsync(int id);
        Task AddPlayerAsync(Player player);
        Task UpdatePlayerAsync(Player player);
        Task DeletePlayerAsync(Player player);
        Task<bool> PlayerExistsAsync(string firstName, string lastName,
            DateTime dob, int? excludeId = null);
        Task<bool> TeamFkExistsAsync(int teamId);
        //MATCH
        Task<IEnumerable<Match>> GetAllMatchesAsync();
        Task<Match?> GetMatchAsync(int id);
        Task AddMatchAsync(Match match);
        Task UpdateMatchAsync(Match match);
        Task DeleteMatchAsync(Match match);
        Task<bool> MatchExistsAsync(DateTime start, DateTime end,
            int homeTeamId, int awayTeamId, int? excludeId = null);
        Task<bool> IsTeamBusyAsync(int teamId, DateTime start, DateTime end, int? excludeMatchId = null);

        //PLAYERMATCHSTATS
        Task<IEnumerable<PlayerMatchStats>> GetAllStatsForPlayerAsync(int playerId);
        Task<PlayerMatchStats?> GetStatsForPlayerFromOneMatchAsync(int playerId, int matchId);

    }
}