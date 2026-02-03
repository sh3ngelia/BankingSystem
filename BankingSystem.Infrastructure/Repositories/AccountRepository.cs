using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Interfaces;
using BankingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Repositories;
public class AccountRepository : IAccountRepository
{
    private readonly BankingDbContext _context;
    public AccountRepository(BankingDbContext context) => _context = context;


    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }


    public async Task<Account?> GetByIdAsync(Guid id) =>
        await _context.Accounts.Include(a => a.Transactions).FirstOrDefaultAsync(a => a.Id == id);


    public async Task<Account?> GetByAccountNumberAsync(string accountNumber) =>
        await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);


    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }
}