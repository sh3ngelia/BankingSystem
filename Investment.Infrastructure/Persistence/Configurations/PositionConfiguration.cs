using Investment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investment.Infrastructure.Persistence.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PortfolioId)
            .IsRequired();

        builder.Property(p => p.AssetId)
            .IsRequired();

        builder.Property(p => p.Symbol)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.Quantity)
            .HasPrecision(18, 6)
            .IsRequired();

        builder.Property(p => p.AverageCost)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Ignore(p => p.TotalCost);
        builder.Ignore(p => p.IsClosed);
    }
}