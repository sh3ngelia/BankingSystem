using Account.Domain.Enums;
using Account.Domain.Events;
using Account.Domain.Exceptions;
using Account.Domain.ValueObjects;
using BankingSystem.Shared.BaseTypes;
using BankingSystem.Shared.Enums;
using BankingSystem.Shared.ValueObjects;

namespace Account.Domain.Entities;

public class Account : AggregateRoot
{
    public AccountNumber AccountNumber { get; private set; }
    public Guid OwnerId { get; private set; }
    public Money Balance { get; private set; }
    public AccountType Type { get; private set; }
    public AccountStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Account() { }

    public static Account Create(Guid ownerId, AccountType type, Currency currency)
    {
        var account = new Account
        {
            AccountNumber = AccountNumber.Generate(),
            OwnerId = ownerId,
            Balance = Money.Zero(currency),
            Type = type,
            Status = AccountStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        account.AddDomainEvent(new AccountCreatedEvent(
            account.Id,
            ownerId.ToString(),
            account.AccountNumber.Value,
            DateTime.UtcNow));

        return account;
    }

    public void Deposit(Money amount)
    {
        if (Status == AccountStatus.Frozen)
            throw new AccountFrozenException(Id);

        if (Status == AccountStatus.Closed)
            throw new InvalidOperationException("Cannot deposit to a closed account.");

        Balance += amount;

        AddDomainEvent(new MoneyDepositedEvent(
            Id,
            amount.Amount,
            amount.Currency.ToString(),
            DateTime.UtcNow));
    }

    public void Withdraw(Money amount)
    {
        if (Status == AccountStatus.Frozen)
            throw new AccountFrozenException(Id);

        if (Status == AccountStatus.Closed)
            throw new InvalidOperationException("Cannot withdraw from a closed account.");

        Balance -= amount;

        AddDomainEvent(new MoneyWithdrawnEvent(
            Id,
            amount.Amount,
            amount.Currency.ToString(),
            DateTime.UtcNow));
    }

    public void Freeze()
    {
        if (Status == AccountStatus.Closed)
            throw new InvalidOperationException("Cannot freeze a closed account.");

        Status = AccountStatus.Frozen;
        AddDomainEvent(new AccountFrozenEvent(Id, DateTime.UtcNow));
    }

    public void Unfreeze()
    {
        if (Status != AccountStatus.Frozen)
            throw new InvalidOperationException("Account is not frozen.");

        Status = AccountStatus.Active;
    }

    public void Close()
    {
        if (Balance.Amount > 0)
            throw new InvalidOperationException("Cannot close account with remaining balance.");

        Status = AccountStatus.Closed;
    }
}