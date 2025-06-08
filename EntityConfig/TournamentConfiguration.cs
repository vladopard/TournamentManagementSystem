using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.EntityConfig
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            //PK
            builder.HasKey(t => t.TournamentId);
            // 2) Scalar properties
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Location) .IsRequired() .HasMaxLength(100);
            builder.Property(t => t.SportType).IsRequired().HasMaxLength(50);
            builder.Property(t => t.StartDate).IsRequired();
            builder.Property(t => t.EndDate).IsRequired();

            builder.HasIndex(t => new { t.StartDate, t.EndDate, t.Name, t.Location, t.SportType })
                .IsUnique();

            // 3) Organizer relationship (many Tournaments → one Organizer)
            //builder.HasOne(t => t.Organizer)
            //    .WithMany(o => o.Tournaments)
            //    .HasForeignKey(t => t.OrganizerId)
            //    .OnDelete(DeleteBehavior.Restrict);
            // 4) Teams collection (one Tournament → many Teams)
            builder.HasMany(t => t.Teams)
                .WithOne(team => team.Tournament)
                .HasForeignKey(team => team.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);
            // 5) Matches collection (one Tournament → many Matches)
            builder.HasMany(t => t.Matches)
                .WithOne(m => m.Tournament)
                .HasForeignKey(m => m.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Tournaments", table =>
            {
                table.HasCheckConstraint(
                    name: "CK_Tournament_Dates",
                    sql: "\"StartDate\" < \"EndDate\"");
            });
        }
    }
}
