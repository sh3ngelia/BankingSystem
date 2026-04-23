using AccountEntity = Account.Domain.Entities.Account;

namespace Account.Application.Abstractions;

public interface IAccountRepository
{
    Task<AccountEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AccountEntity?> GetByAccountNumberAsync(string accountNumber, CancellationToken ct = default);
    Task<IEnumerable<AccountEntity>> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default);
    Task AddAsync(AccountEntity account, CancellationToken ct = default);
    Task UpdateAsync(AccountEntity account, CancellationToken ct = default);
}