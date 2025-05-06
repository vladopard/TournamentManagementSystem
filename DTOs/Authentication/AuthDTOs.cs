namespace TournamentManagementSystem.DTOs.Authentication
{
    public class AuthRegisterDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public class AuthLoginDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AuthResultDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
    }
}
