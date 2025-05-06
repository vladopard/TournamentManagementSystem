namespace TournamentManagementSystem.DTOs.Authentication
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateJoined { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
