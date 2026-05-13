namespace TournamentManagementSystem.DTOs.Parameters
{
    public class MatchParameters : QueryParameters
    {
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public int? TournamentId { get; set; }
    }
}
