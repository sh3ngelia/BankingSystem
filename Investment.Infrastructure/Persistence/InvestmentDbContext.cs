using Investment.Domain.Entities;
using Investment.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Investment.Infrastructure.Persistence;

public class InvestmentDbContext : DbContext
{
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Asset> Assets { get; set; }

    public InvestmentDbContext(DbContextOptions<InvestmentDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
        modelBuilder.ApplyConfiguration(new PositionConfiguration());
        modelBuilder.ApplyConfiguration(new AssetConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}