using Transaction.Application.Abstractions;

namespace Transaction.Application.Commands.FailTransaction;

public record FailTransactionCommand(
    Guid TransactionId,
    string Reason) : ICommand;