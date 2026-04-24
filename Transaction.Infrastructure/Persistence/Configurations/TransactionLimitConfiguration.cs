using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Domain.Entities;

namespace Transaction.Infrastructure.Persistence.Configurations;

public class TransactionLimitConfiguration : IEntityTypeConfiguration<TransactionLimit>
{
    public void Configure(EntityTypeBuilder<TransactionLimit> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.AccountId)
            .IsRequired();

        builder.HasIndex(t => t.AccountId)
            .IsUnique();

        builder.Property(t => t.DailyLimit)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.MonthlyLimit)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.DailyUsed)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.MonthlyUsed)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.LastResetDate)
            .IsRequired();
    }
}