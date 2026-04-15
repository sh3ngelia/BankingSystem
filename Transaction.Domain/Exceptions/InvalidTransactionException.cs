namespace Transaction.Domain.Exceptions;

public class InvalidTransactionException : Exception
{
    public InvalidTransactionException(string reason)
        : base($"Invalid transaction: {reason}") { }
}