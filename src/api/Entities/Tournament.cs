using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentManagementSystem.Entities
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string Location { get; set; }
        public required string SportType { get; set; } // E.g., Soccer, Basketball

        // Relationships
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = null!;
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<Match> Matches { get; set; } = new List<Match>();
    }
}
