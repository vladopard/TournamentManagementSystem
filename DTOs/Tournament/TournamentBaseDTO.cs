namespace TournamentManagementSystem.DTOs
{
    public abstract class TournamentBaseDTO
    {
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = null!;
        public string SportType { get; set; } = null!;
        public int OrganizerId { get; set; }
    }
}
