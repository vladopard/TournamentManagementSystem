using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.EntityConfig
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(m => m.MatchId);

            builder.Property(m => m.StartDate).IsRequired();
            builder.Property(m => m.EndDate).IsRequired();
            builder.Property(m => m.ScoreHome).IsRequired();
            builder.Property(m => m.ScoreAway).IsRequired();

            builder.HasIndex(m => new { m.StartDate, m.EndDate, m.HomeTeamId,m.AwayTeamId }).IsUnique();

            builder.HasOne(m => m.HomeTeam)
            .WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.AwayTeam)
           .WithMany(t => t.AwayMatches)
           .HasForeignKey(m => m.AwayTeamId)
           .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(m => m.Tournament)
            //    .WithMany(t => t.Matches)
            //    .HasForeignKey(m => m.TournamentId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.PlayerStats)
                .WithOne(pms => pms.Match)
                .HasForeignKey(pms => pms.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Matches", table =>
            {
                table.HasCheckConstraint(
                    "CK_Match_Dates",
                    "\"StartDate\" < \"EndDate\"");
            });

        }
    }
}
