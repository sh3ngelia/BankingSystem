using Card.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Card.Infrastructure.Persistence.Repositories;

public class CardRepository : ICardRepository
{
    private readonly CardDbContext _context;

    public CardRepository(CardDbContext context)
    {
        _context = context;
    }

    public async Task<Card.Domain.Entities.Card?> GetByIdAsync(
        Guid id, CancellationToken ct = default)
    {
        return await _context.Cards
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IEnumerable<Card.Domain.Entities.Card>> GetByAccountIdAsync(
        Guid accountId, CancellationToken ct = default)
    {
        return await _context.Cards
            .Where(c => c.AccountId == accountId)
            .OrderByDescending(c => c.IssuedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(
        Card.Domain.Entities.Card card, CancellationToken ct = default)
    {
        await _context.Cards.AddAsync(card, ct);
    }

    public async Task UpdateAsync(
        Card.Domain.Entities.Card card, CancellationToken ct = default)
    {
        _context.Cards.Update(card);
        await Task.CompletedTask;
    }
}