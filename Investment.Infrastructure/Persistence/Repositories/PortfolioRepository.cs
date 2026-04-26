using Investment.Application.Abstractions;
using Investment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Investment.Infrastructure.Persistence.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly InvestmentDbContext _context;

    public PortfolioRepository(InvestmentDbContext context)
    {
        _context = context;
    }

    public async Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Portfolios
            .Include(p => p.Positions)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<IEnumerable<Portfolio>> GetByOwnerIdAsync(
        Guid ownerId, CancellationToken ct = default)
    {
        return await _context.Portfolios
            .Where(p => p.OwnerId == ownerId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Portfolio portfolio, CancellationToken ct = default)
    {
        await _context.Portfolios.AddAsync(portfolio, ct);
    }

    public async Task UpdateAsync(Portfolio portfolio, CancellationToken ct = default)
    {
        _context.Portfolios.Update(portfolio);
        await Task.CompletedTask;
    }
}