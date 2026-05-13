using TournamentManagementSystem.DTOs.Player;

namespace TournamentManagementSystem.DTOs.Team
{
    public abstract class TeamBaseDTO
    {
        public required string Name { get; set; }
        public required string Coach { get; set; }
        public int TournamentId { get; set; }
    }

    public class TeamCreateDTO : TeamBaseDTO { }
    public class TeamUpdateDTO : TeamBaseDTO { }
    public class TeamDTO : TeamBaseDTO
    {
        public int TeamId { get; set; }

        public required string TournamentName { get; set; }

        // you can also flatten player counts or list of PlayerDTOs:
        public IEnumerable<PlayerDTO> Players { get; set; } = new List<PlayerDTO>();
    }
    public class TeamPatchDTO
    {
        public string? Name { get; set; }
        public string? Coach { get; set; }
        public int? TournamentId { get; set; }
    }
}
