using Loan.Domain.Entities;
using Loan.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Loan.Infrastructure.Persistence;

public class LoanDbContext : DbContext
{
    public DbSet<Loan.Domain.Entities.Loan> Loans { get; set; }
    public DbSet<RepaymentSchedule> RepaymentSchedules { get; set; }
    public DbSet<Installment> Installments { get; set; }

    public LoanDbContext(DbContextOptions<LoanDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LoanConfiguration());
        modelBuilder.ApplyConfiguration(new RepaymentScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new InstallmentConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}