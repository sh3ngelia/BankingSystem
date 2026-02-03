using BankingSystem.Domain.Entities;

namespace BankingSystem.Domain.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);
    Task<Account?> GetByAccountNumberAsync(string accountNumber);
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
}