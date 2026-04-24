using Transaction.Application.Abstractions;
using Transaction.Application.DTOs;

namespace Transaction.Application.Queries.GetTransaction;

public class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, TransactionDto?>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionDto?> Handle(GetTransactionQuery query, CancellationToken ct)
    {
        var transaction = await _transactionRepository.GetByIdAsync(query.TransactionId, ct);
        if (transaction is null) return null;

        return new TransactionDto(
            transaction.Id,
            transaction.FromAccountId,
            transaction.ToAccountId,
            transaction.Amount,
            transaction.Currency,
            transaction.Type.ToString(),
            transaction.Status.ToString(),
            transaction.FailureReason,
            transaction.CreatedAt,
            transaction.CompletedAt);
    }
}