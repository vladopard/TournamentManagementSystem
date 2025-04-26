using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.EntityConfig
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.PlayerId);

            // 2) Scalar properties
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Position).IsRequired().HasMaxLength(50);
            builder.Property(p => p.DateOfBirth).IsRequired();

            //builder.HasOne(p => p.Team)
            //       .WithMany(t => t.Players)
            //       .HasForeignKey(p => p.TeamId)
            //       .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.MatchStats)
                .WithOne(pms => pms.Player)
                .HasForeignKey(pms => pms.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
