using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
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

            //A database index is like creating a separate, smaller table
            //that helps the database find the data much faster.
            builder.HasIndex(p => new { p.FirstName,p.LastName,p.DateOfBirth }).IsUnique();
            //That line of code tells Entity Framework Core to:
            //Create a database object: Create a database object called an "index".
            //Index these columns: This index will be created using the data
            //from the FirstName, LastName, and DateOfBirth columns of the Players table.
            //Make it unique: The IsUnique() part adds a constraint to the index.
            //This constraint forces the database to ensure that no two rows in the Players table
            //have the same combination of values for those three columns.

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
