using Transaction.Application.Abstractions;
using Transaction.Application.DTOs;

namespace Transaction.Application.Queries.GetAccountTransactions;

public class GetAccountTransactionsQueryHandler
    : IQueryHandler<GetAccountTransactionsQuery, IEnumerable<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetAccountTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<TransactionDto>> Handle(
        GetAccountTransactionsQuery query,
        CancellationToken ct)
    {
        var transactions = await _transactionRepository.GetByAccountIdAsync(query.AccountId, ct);

        return transactions.Select(t => new TransactionDto(
            t.Id,
            t.FromAccountId,
            t.ToAccountId,
            t.Amount,
            t.Currency,
            t.Type.ToString(),
            t.Status.ToString(),
            t.FailureReason,
            t.CreatedAt,
            t.CompletedAt));
    }
}