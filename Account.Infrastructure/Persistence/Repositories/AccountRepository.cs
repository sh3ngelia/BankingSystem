using Account.Application.Abstractions;
using Account.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountDbContext _context;
    private readonly string _connectionString;

    public AccountRepository(AccountDbContext context, string connectionString)
    {
        _context = context;
        _connectionString = connectionString;
    }

    public async Task<Account.Domain.Entities.Account?> GetByIdAsync(
        Guid id, CancellationToken ct = default)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<Account.Domain.Entities.Account?> GetByAccountNumberAsync(
        string accountNumber, CancellationToken ct = default)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber ==
                Account.Domain.ValueObjects.AccountNumber.Create(accountNumber), ct);
    }

    public async Task<IEnumerable<Account.Domain.Entities.Account>> GetByOwnerIdAsync(
        Guid ownerId, CancellationToken ct = default)
    {
        return await _context.Accounts
            .Where(a => a.OwnerId == ownerId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(
        Account.Domain.Entities.Account account, CancellationToken ct = default)
    {
        await _context.Accounts.AddAsync(account, ct);
    }

    public async Task UpdateAsync(
        Account.Domain.Entities.Account account, CancellationToken ct = default)
    {
        _context.Accounts.Update(account);
        await Task.CompletedTask;
    }
}