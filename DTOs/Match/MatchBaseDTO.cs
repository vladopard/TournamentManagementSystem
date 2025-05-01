using TournamentManagementSystem.DTOs.Team;

namespace TournamentManagementSystem.DTOs.Match
{

    public abstract class MatchBaseDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ScoreHome { get; set; }
        public int ScoreAway { get; set; }
        public int TournamentId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
    }
    public class MatchCreateDTO : MatchBaseDTO { }
    public class MatchUpdateDTO : MatchBaseDTO { }

    public class MatchDTO : MatchBaseDTO
    {
        public int MatchId { get; set; }
        // optionally include nested TeamDTOs:
        public required string HomeTeamName { get; set; }
        public required string AwayTeamName { get; set; }
    }
    public class MatchPatchDTO
    {
        // same props, but nullable for patching
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ScoreHome { get; set; }
        public int? ScoreAway { get; set; }
        public int? TournamentId { get; set; }
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
    }
}
