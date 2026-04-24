using Transaction.Application.Abstractions;

namespace Transaction.Application.Commands.CompleteTransaction;

public record CompleteTransactionCommand(Guid TransactionId) : ICommand;