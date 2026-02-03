using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Interfaces;
using BankingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Repositories;
public class TransactionRepository : ITransactionRepository
{
    private readonly BankingDbContext _context;
    public TransactionRepository(BankingDbContext context) => _context = context;


    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }


    public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId) =>
        await _context.Transactions.Where(t => t.AccountId == accountId).ToListAsync();
}