using Loan.Domain.Entities;
using Loan.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.Infrastructure.Persistence.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan.Domain.Entities.Loan>
{
    public void Configure(EntityTypeBuilder<Loan.Domain.Entities.Loan> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.ApplicantId)
            .IsRequired();

        builder.Property(l => l.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        // InterestRate Value Object
        builder.Property(l => l.InterestRate)
            .HasPrecision(5, 2)
            .IsRequired()
            .HasConversion(
                ir => ir.Value,
                value => InterestRate.Create(value));

        builder.Property(l => l.TermMonths)
            .IsRequired();

        builder.Property(l => l.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(l => l.Status)
            .HasConversion<string>()
            .IsRequired();

        // CreditScore Value Object — nullable
        builder.Property(l => l.CreditScore)
            .HasConversion(
                cs => cs != null ? cs.Value : (int?)null,
                value => value != null ? CreditScore.Create(value.Value) : null);

        builder.Property(l => l.RejectionReason)
            .HasMaxLength(500);

        builder.Property(l => l.CreatedAt)
            .IsRequired();

        builder.Property(l => l.ApprovedAt);

        // RepaymentSchedule — one-to-one
        builder.HasOne(l => l.RepaymentSchedule)
            .WithOne()
            .HasForeignKey<RepaymentSchedule>(r => r.LoanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(l => l.DomainEvents);
    }
}