using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.EntityConfig
{
    public class PlayerMatchStatsConfiguration : IEntityTypeConfiguration<PlayerMatchStats>
    {
        public void Configure(EntityTypeBuilder<PlayerMatchStats> builder)
        {
            // 1) Primary Key
            builder.HasKey(pms => new { pms.MatchId, pms.PlayerId });

            // 2) Scalar properties
            builder.Property(pms => pms.PointsScored).IsRequired();
            builder.Property(pms => pms.MatchRating).IsRequired();
            builder.Property(pms => pms.FoulsCommited).IsRequired();


        }
    }
}
