using System.Text.RegularExpressions;

namespace TournamentManagementSystem.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Position { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Relationships
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;
        public List<PlayerMatchStats> MatchStats { get; set; } = new();
    }
}
