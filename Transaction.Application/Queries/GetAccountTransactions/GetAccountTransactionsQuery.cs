using Transaction.Application.Abstractions;
using Transaction.Application.DTOs;

namespace Transaction.Application.Queries.GetAccountTransactions;

public record GetAccountTransactionsQuery(Guid AccountId) : IQuery<IEnumerable<TransactionDto>>;