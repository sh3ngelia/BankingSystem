using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Transaction.Application.Abstractions;

namespace Transaction.Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly TransactionDbContext _context;
    private readonly string _connectionString;

    public TransactionRepository(TransactionDbContext context, string connectionString)
    {
        _context = context;
        _connectionString = connectionString;
    }

    public async Task<Transaction.Domain.Entities.Transaction?> GetByIdAsync(
        Guid id, CancellationToken ct = default)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<IEnumerable<Transaction.Domain.Entities.Transaction>> GetByAccountIdAsync(
        Guid accountId, CancellationToken ct = default)
    {
        // Dapper — სწრაფი კითხვა ორივე მიმართულებით
        using var connection = new SqlConnection(_connectionString);

        const string sql = """
            SELECT Id, FromAccountId, ToAccountId, Amount, Currency,
                   Type, Status, FailureReason, CreatedAt, CompletedAt
            FROM Transactions
            WHERE FromAccountId = @AccountId OR ToAccountId = @AccountId
            ORDER BY CreatedAt DESC
            """;

        var results = await connection.QueryAsync<TransactionQueryResult>(
            sql, new { AccountId = accountId });

        return results.Select(r =>
        {
            var t = Transaction.Domain.Entities.Transaction.Initiate(
                r.FromAccountId,
                r.ToAccountId,
                r.Amount,
                r.Currency,
                Enum.Parse<Transaction.Domain.Enums.TransactionType>(r.Type));

            return t;
        });
    }

    public async Task AddAsync(
        Transaction.Domain.Entities.Transaction transaction, CancellationToken ct = default)
    {
        await _context.Transactions.AddAsync(transaction, ct);
    }

    public async Task UpdateAsync(
        Transaction.Domain.Entities.Transaction transaction, CancellationToken ct = default)
    {
        _context.Transactions.Update(transaction);
        await Task.CompletedTask;
    }

    private record TransactionQueryResult(
        Guid Id,
        Guid FromAccountId,
        Guid ToAccountId,
        decimal Amount,
        string Currency,
        string Type,
        string Status,
        string? FailureReason,
        DateTime CreatedAt,
        DateTime? CompletedAt);
}