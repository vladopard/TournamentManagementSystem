namespace TournamentManagementSystem.DTOs.Organizer
{
    public class OrganizerDTO : OrganizerBaseDTO
    {
        public int OrganizerId { get; set; }
        public List<TournamentDTO> Tournaments { get; set; } = new();
    }
}
