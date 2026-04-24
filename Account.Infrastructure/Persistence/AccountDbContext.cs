using Account.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Persistence;

public class AccountDbContext : DbContext
{
    public DbSet<Account.Domain.Entities.Account> Accounts { get; set; }

    public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}