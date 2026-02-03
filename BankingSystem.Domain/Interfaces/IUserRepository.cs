using BankingSystem.Domain.Entities;

namespace BankingSystem.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}