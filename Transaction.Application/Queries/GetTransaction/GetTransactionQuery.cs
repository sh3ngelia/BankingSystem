using Transaction.Application.Abstractions;
using Transaction.Application.DTOs;

namespace Transaction.Application.Queries.GetTransaction;

public record GetTransactionQuery(Guid TransactionId) : IQuery<TransactionDto?>;