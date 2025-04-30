using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.EntityConfig
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            builder.HasKey(o => o.OrganizerId);

            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
            builder.Property(o => o.ContactInfo).IsRequired().HasMaxLength(200);

            builder.HasIndex(o => new { o.Name, o.ContactInfo }).IsUnique();

            builder.HasMany(o => o.Tournaments)
                .WithOne(t => t.Organizer)
                .HasForeignKey(t => t.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
