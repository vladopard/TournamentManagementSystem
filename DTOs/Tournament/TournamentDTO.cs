namespace TournamentManagementSystem.DTOs
{
    public class TournamentDTO : TournamentBaseDTO
    {
        public int TournamentId { get; set; }
        //public string Name { get; set; } = null!;
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public string Location { get; set; } = null!;
        //public string SportType { get; set; } = null!;
        //public int? OrganizerId { get; set; }
        public string OrganizerName { get; set; } = null!;
        public List<string> TeamNames { get; set; } = new();
    }
}
