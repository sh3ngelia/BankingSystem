using Card.Domain.Entities;
using Card.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Card.Infrastructure.Persistence.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card.Domain.Entities.Card>
{
    public void Configure(EntityTypeBuilder<Card.Domain.Entities.Card> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.AccountId)
            .IsRequired();

        builder.Property(c => c.CardNumber)
            .IsRequired()
            .HasMaxLength(16)
            .HasConversion(
                cn => cn.Value,
                value => CardNumber.Create(value));

        builder.HasIndex(c => c.CardNumber)
            .IsUnique();

        builder.Property(c => c.CVV)
            .IsRequired()
            .HasMaxLength(3);

        builder.OwnsOne(c => c.ExpiryDate, expiry =>
        {
            expiry.Property(e => e.Month)
                .HasColumnName("ExpiryMonth")
                .IsRequired();

            expiry.Property(e => e.Year)
                .HasColumnName("ExpiryYear")
                .IsRequired();
        });

        builder.Property(c => c.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.DailyLimit)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.MonthlyLimit)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.IssuedAt)
            .IsRequired();

        // CardTransactions - one-to-many
        builder.HasMany(c => c.Transactions)
            .WithOne()
            .HasForeignKey(ct => ct.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(c => c.DomainEvents);
    }
}