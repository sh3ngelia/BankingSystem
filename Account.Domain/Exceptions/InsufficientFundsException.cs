namespace Account.Domain.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(decimal required, decimal available)
        : base($"Insufficient funds. Required: {required}, Available: {available}.") { }
}