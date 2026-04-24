using Account.Domain.Entities;
using Account.Domain.Enums;
using Account.Domain.ValueObjects;
using BankingSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account.Domain.Entities.Account>
{
    public void Configure(EntityTypeBuilder<Account.Domain.Entities.Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.OwnerId)
            .IsRequired();

        builder.Property(a => a.AccountNumber)
            .IsRequired()
            .HasMaxLength(34)
            .HasConversion(
                an => an.Value,
                value => AccountNumber.Create(value));

        builder.HasIndex(a => a.AccountNumber)
            .IsUnique();

        // Money Value Object — ორ column-ად ინახება
        builder.OwnsOne(a => a.Balance, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("Balance")
                .HasPrecision(18, 2)
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasConversion<string>()
                .IsRequired();
        });

        builder.Property(a => a.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Ignore(a => a.DomainEvents);
    }
}