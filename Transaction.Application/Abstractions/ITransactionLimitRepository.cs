using Transaction.Domain.Entities;

namespace Transaction.Application.Abstractions;

public interface ITransactionLimitRepository
{
    Task<TransactionLimit?> GetByAccountIdAsync(Guid accountId, CancellationToken ct = default);
    Task AddAsync(TransactionLimit limit, CancellationToken ct = default);
    Task UpdateAsync(TransactionLimit limit, CancellationToken ct = default);
}