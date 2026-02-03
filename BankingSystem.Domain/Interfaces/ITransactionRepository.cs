using BankingSystem.Domain.Entities;

namespace BankingSystem.Domain.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId);
}