namespace TournamentManagementSystem.DTOs.Player
{
    public abstract class PlayerBaseDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TeamId { get; set; }    
    }

    public class PlayerCreateDTO : PlayerBaseDTO { }
    public class PlayerUpdateDTO : PlayerBaseDTO { }

    public class PlayerPatchDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? TeamId { get; set; }
    }

    // ── 4) Read DTO includes the generated key + team name
    public class PlayerDTO : PlayerBaseDTO
    {
        public int PlayerId { get; set; }
        public string TeamName { get; set; } = null!;
    }
}
