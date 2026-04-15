namespace Transaction.Domain.Exceptions;

public class TransactionLimitExceededException : Exception
{
    public TransactionLimitExceededException(decimal limit, string limitType)
        : base($"{limitType} transaction limit of {limit} exceeded.") { }
}