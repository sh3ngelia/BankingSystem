using Transaction.Application.Abstractions;

namespace Transaction.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TransactionDbContext _context;

    public UnitOfWork(TransactionDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}