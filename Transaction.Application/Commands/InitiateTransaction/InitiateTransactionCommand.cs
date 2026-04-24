using Transaction.Application.Abstractions;
using Transaction.Domain.Enums;

namespace Transaction.Application.Commands.InitiateTransaction;

public record InitiateTransactionCommand(
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    string Currency,
    TransactionType Type) : ICommand<Guid>;