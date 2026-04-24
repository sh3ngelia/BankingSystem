using Transaction.Application.Abstractions;

namespace Transaction.Application.Commands.ReverseTransaction;

public record ReverseTransactionCommand(
    Guid TransactionId,
    string Reason) : ICommand;