using Loan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.Infrastructure.Persistence.Configurations;

public class RepaymentScheduleConfiguration : IEntityTypeConfiguration<RepaymentSchedule>
{
    public void Configure(EntityTypeBuilder<RepaymentSchedule> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.LoanId)
            .IsRequired();

        builder.Property(r => r.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(r => r.TotalInterest)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasMany(r => r.Installments)
            .WithOne()
            .HasForeignKey(i => i.LoanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(r => r.RemainingInstallments);
        builder.Ignore(r => r.RemainingAmount);
    }
}