using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.EntityConfig
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.TeamId);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Coach).IsRequired().HasMaxLength(100);

            builder.HasIndex(t => new { t.Name,t.TournamentId }).IsUnique();

            builder.HasOne(t => t.Tournament)
                .WithMany(tr => tr.Teams)
                .HasForeignKey(t => t.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Players)
                   .WithOne(p => p.Team)
                   .HasForeignKey(p => p.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(t => t.Matches);
        }
    }
}
