namespace Investment.Domain.Exceptions;

public class InsufficientSharesException : Exception
{
    public InsufficientSharesException(string symbol, decimal requested, decimal available)
        : base($"Insufficient shares of '{symbol}'. Requested: {requested}, Available: {available}.") { }
}