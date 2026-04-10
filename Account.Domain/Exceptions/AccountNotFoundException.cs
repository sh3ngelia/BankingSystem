namespace Account.Domain.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(Guid accountId)
        : base($"Account with id '{accountId}' was not found.") { }

    public AccountNotFoundException(string accountNumber)
        : base($"Account with number '{accountNumber}' was not found.") { }
}