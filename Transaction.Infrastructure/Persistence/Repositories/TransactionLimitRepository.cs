using Microsoft.EntityFrameworkCore;
using Transaction.Application.Abstractions;
using Transaction.Domain.Entities;

namespace Transaction.Infrastructure.Persistence.Repositories;

public class TransactionLimitRepository : ITransactionLimitRepository
{
    private readonly TransactionDbContext _context;

    public TransactionLimitRepository(TransactionDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionLimit?> GetByAccountIdAsync(
        Guid accountId, CancellationToken ct = default)
    {
        return await _context.TransactionLimits
            .FirstOrDefaultAsync(l => l.AccountId == accountId, ct);
    }

    public async Task AddAsync(TransactionLimit limit, CancellationToken ct = default)
    {
        await _context.TransactionLimits.AddAsync(limit, ct);
    }

    public async Task UpdateAsync(TransactionLimit limit, CancellationToken ct = default)
    {
        _context.TransactionLimits.Update(limit);
        await Task.CompletedTask;
    }
}