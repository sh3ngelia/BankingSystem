using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Data;
public class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }


    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();


        modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique();


        modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId);


        modelBuilder.Entity<User>()
            .HasMany(u => u.Accounts)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
    }
}