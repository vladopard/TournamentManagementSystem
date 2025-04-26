namespace TournamentManagementSystem.Entities
{
    public class Organizer
    {
        public int OrganizerId { get; set; }
        public required string Name { get; set; }
        public required string ContactInfo { get; set; }

        // Relationships
        public List<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
