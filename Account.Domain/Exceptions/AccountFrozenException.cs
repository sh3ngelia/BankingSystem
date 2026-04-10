namespace Account.Domain.Exceptions;

public class AccountFrozenException : Exception
{
    public AccountFrozenException(Guid accountId)
        : base($"Account '{accountId}' is frozen.") { }
}