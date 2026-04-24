using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(r => r.Name)
            .IsUnique();

        builder.HasData(
            new { Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), Name = "Admin" },
            new { Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"), Name = "Customer" },
            new { Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-123456789012"), Name = "Investor" }
        );
    }
}