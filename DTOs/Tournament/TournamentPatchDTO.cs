namespace TournamentManagementSystem.DTOs.Tournament
{
    public class TournamentPatchDTO
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public string? SportType { get; set; }
        public int? OrganizerId { get; set; }
    }
}
