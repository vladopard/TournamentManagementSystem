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
    }
}