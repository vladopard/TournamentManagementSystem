using Microsoft.AspNetCore.Identity;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}