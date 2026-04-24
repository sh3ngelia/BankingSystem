using Loan.Application.Abstractions;

namespace Loan.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly LoanDbContext _context;

    public UnitOfWork(LoanDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}