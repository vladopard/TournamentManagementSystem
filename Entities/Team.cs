using System.Numerics;
using System.Text.RegularExpressions;

namespace TournamentManagementSystem.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public required string Name { get; set; }
        public required string Coach { get; set; }

        // Relationships
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Match> Matches { get; set; } = new List<Match>();
    }
}
