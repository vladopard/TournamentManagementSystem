using Microsoft.EntityFrameworkCore;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.DbContexts
{
    public class TournamentDbContext : DbContext
    {
        public TournamentDbContext(DbContextOptions<TournamentDbContext> options) 
            : base(options) { }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<PlayerMatchStats> Results { get; set; }

    }
}
