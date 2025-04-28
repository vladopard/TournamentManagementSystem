namespace TournamentManagementSystem.DTOs
{
    public class TournamentDTO
    {
        public int TournamentId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = null!;
        public string SportType { get; set; } = null!;
        public string OrganizerName { get; set; } = null!;
        public int? OrganizerId { get; set; }
        public List<string> TeamNames { get; set; } = new();
    }
}
