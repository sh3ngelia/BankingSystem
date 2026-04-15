namespace Transaction.Domain.Exceptions;

public class TransactionNotFoundException : Exception
{
    public TransactionNotFoundException(Guid transactionId)
        : base($"Transaction with id '{transactionId}' was not found.") { }
}