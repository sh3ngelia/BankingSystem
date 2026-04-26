using Card.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Card.Infrastructure.Persistence.Configurations;

public class CardTransactionConfiguration : IEntityTypeConfiguration<CardTransaction>
{
    public void Configure(EntityTypeBuilder<CardTransaction> builder)
    {
        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.CardId)
            .IsRequired();

        builder.Property(ct => ct.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(ct => ct.Currency)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(ct => ct.MerchantName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ct => ct.MerchantCategory)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ct => ct.CreatedAt)
            .IsRequired();
    }
}