using Card.Application.Abstractions;

namespace Card.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CardDbContext _context;

    public UnitOfWork(CardDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}