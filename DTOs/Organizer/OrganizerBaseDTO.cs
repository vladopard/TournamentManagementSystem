namespace TournamentManagementSystem.DTOs.Organizer
{
    public abstract class OrganizerBaseDTO
    {
        public string Name { get; set; } = null!;
        public string ContactInfo { get; set; } = null!;
 
    }
    public class OrganizerCreateDTO : OrganizerBaseDTO { }
    public class OrganizerUpdateDTO : OrganizerBaseDTO { }

    public class OrganizerDTO : OrganizerBaseDTO
    {
        public int OrganizerId { get; set; }
        public List<TournamentDTO> Tournaments { get; set; } = new();
    }

    public class OrganizerPatchDTO
    {
        public string? Name { get; set; }
        public string? ContactInfo { get; set; }
    }
}
