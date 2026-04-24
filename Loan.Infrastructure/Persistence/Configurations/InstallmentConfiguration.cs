using Loan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.Infrastructure.Persistence.Configurations;

public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.LoanId)
            .IsRequired();

        builder.Property(i => i.InstallmentNumber)
            .IsRequired();

        builder.Property(i => i.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.PrincipalPart)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.InterestPart)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.DueDate)
            .IsRequired();

        builder.Property(i => i.IsPaid)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(i => i.PaidAt);
    }
}