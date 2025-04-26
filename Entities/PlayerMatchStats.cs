namespace TournamentManagementSystem.Entities
{
    //juction player -> <- match
    public class PlayerMatchStats
    {
        public int PlayerMatchStatsId { get; set; }
        public int GoalsScored { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        //Relations
        public int MatchId { get; set; }
        public Match Match { get; set; } = null!;

        public int PlayerId { get; set; }
        public Player Player { get; set; } = null!;
        
    }
}
