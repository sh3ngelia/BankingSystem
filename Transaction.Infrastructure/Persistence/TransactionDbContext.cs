using Microsoft.EntityFrameworkCore;
using Transaction.Infrastructure.Persistence.Configurations;

namespace Transaction.Infrastructure.Persistence;

public class TransactionDbContext : DbContext
{
    public DbSet<Transaction.Domain.Entities.Transaction> Transactions { get; set; }
    public DbSet<Transaction.Domain.Entities.TransactionLimit> TransactionLimits { get; set; }

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionLimitConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}