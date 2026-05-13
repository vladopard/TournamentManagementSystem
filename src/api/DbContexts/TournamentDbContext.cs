using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.DbContexts
{
    public class TournamentDbContext : IdentityDbContext<ApplicationUser>
    {
        public TournamentDbContext(DbContextOptions<TournamentDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentDbContext).Assembly);

            SeedData.Seed(modelBuilder);
        }


        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<PlayerMatchStats> Results { get; set; }

    }
}
