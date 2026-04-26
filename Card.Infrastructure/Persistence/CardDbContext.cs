using Card.Domain.Entities;
using Card.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Card.Infrastructure.Persistence;

public class CardDbContext : DbContext
{
    public DbSet<Card.Domain.Entities.Card> Cards { get; set; }
    public DbSet<CardTransaction> CardTransactions { get; set; }

    public CardDbContext(DbContextOptions<CardDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new CardTransactionConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}