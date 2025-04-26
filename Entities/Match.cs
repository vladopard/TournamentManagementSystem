namespace TournamentManagementSystem.Entities
{
    public class Match
    {
        public int MatchId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ScoreHome { get; set; }
        public int ScoreAway { get; set; }

        // Relationships
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public Team HomeTeam { get; set; } = null!;
        public Team AwayTeam { get; set; } = null!;
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;
        public List<PlayerMatchStats> PlayerStats { get; set; } = new();
    }
}
