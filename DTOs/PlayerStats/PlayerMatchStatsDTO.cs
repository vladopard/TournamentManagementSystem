namespace TournamentManagementSystem.DTOs.PlayerStats
{
    public class PlayerMatchStatsDTO
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Opponent { get; set; } = null!;
        public int PointsScored { get; set; }
        public decimal MatchRating { get; set; }
        public int FoulsCommited { get; set; }
    }
}
