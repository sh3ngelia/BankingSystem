using Investment.Application.Abstractions;

namespace Investment.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly InvestmentDbContext _context;

    public UnitOfWork(InvestmentDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}