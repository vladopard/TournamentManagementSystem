using Microsoft.AspNetCore.Identity;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.DbContexts
{
    public static class IdentitySeed
    {
        public static async Task EnsureSeedDataAsync(this IServiceProvider services)
        {
            //servisi za manageovanje role-ova i usera
            var userMgr = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

            //These methods like roleExistsAsync, findbyemailasync
            //are part of the UserManager<TUser> and RoleManager<TRole> classes
            //provided by the Microsoft.AspNetCore.Identity framework itself.

            // 1) create roles
            var roles = new[] { "Admin", "Organizer", "Player" };
            foreach (var role in roles)
            {
                if (!await roleMgr.RoleExistsAsync(role))
                    await roleMgr.CreateAsync(new IdentityRole(role));
            }

            // 2) create super-admin
            var adminEmail = "admin@example.com";
            var admin = await userMgr.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Site",
                    LastName = "Admin",
                    DateJoined = DateTime.UtcNow
                };
                var res = await userMgr.CreateAsync(admin, "P@ssw0rd1");
                if (!res.Succeeded)
                    throw new Exception($"Failed to create admin user: {string.Join(", ", res.Errors.Select(e => e.Description))}");
            }

            // 3) assign Admin role
            if (!await userMgr.IsInRoleAsync(admin, "Admin"))
                await userMgr.AddToRoleAsync(admin, "Admin");

        }
    }
}
