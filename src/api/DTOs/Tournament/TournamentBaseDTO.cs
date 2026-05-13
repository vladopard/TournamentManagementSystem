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

    public class TournamentCreateDTO : TournamentBaseDTO {}
    public class TournamentUpdateDTO : TournamentBaseDTO {}

    public class TournamentDTO : TournamentBaseDTO
    {
        public int TournamentId { get; set; }
        public string OrganizerName { get; set; } = null!;
        public List<string> TeamNames { get; set; } = new();
    }

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
