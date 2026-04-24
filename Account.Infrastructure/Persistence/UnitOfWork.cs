using Account.Application.Abstractions;

namespace Account.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountDbContext _context;

    public UnitOfWork(AccountDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}