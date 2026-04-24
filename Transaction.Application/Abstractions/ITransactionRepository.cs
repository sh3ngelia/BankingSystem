using TransactionEntity = Transaction.Domain.Entities.Transaction;

namespace Transaction.Application.Abstractions;

public interface ITransactionRepository
{
    Task<TransactionEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<TransactionEntity>> GetByAccountIdAsync(Guid accountId, CancellationToken ct = default);
    Task AddAsync(TransactionEntity transaction, CancellationToken ct = default);
    Task UpdateAsync(TransactionEntity transaction, CancellationToken ct = default);
}