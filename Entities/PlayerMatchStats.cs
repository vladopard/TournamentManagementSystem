namespace TournamentManagementSystem.Entities
{
    //juction player -> <- match
    public class PlayerMatchStats
    {
        public int PointsScored { get; set; }
        public int MatchRating { get; set; }
        public int FoulsCommited { get; set; }
        //Relations
        public int MatchId { get; set; }
        public Match Match { get; set; } = null!;

        public int PlayerId { get; set; }
        public Player Player { get; set; } = null!;
        
    }
}
