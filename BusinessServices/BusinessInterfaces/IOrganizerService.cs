using TournamentManagementSystem.DTOs.Organizer;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.BusinessServices
{
    public interface IOrganizerService
    {
        Task<IEnumerable<OrganizerDTO>> GetAllOrganizersAsync();
        Task<OrganizerDTO> GetOrganizerAsync(int id);
        Task<OrganizerDTO> CreateOrganizerAsync(OrganizerCreateDTO organizerCreateDTO);
        Task DeleteOrganizerAsync(int id);
        Task PatchOrganizerAsync(OrganizerPatchDTO organizerPatched, int id);
        Task UpdateOrganizerAsync(OrganizerUpdateDTO organizerUpdateDTO, int id);
    }
}