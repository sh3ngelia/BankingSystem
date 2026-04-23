using BankingSystem.Shared.Abstractions;
using BankingSystem.Shared.Enums;
using BankingSystem.Shared.Exceptions;

namespace BankingSystem.Shared.ValueObjects;

public sealed class Money : IValueObject
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.");

        return new Money(amount, currency);
    }

    public static Money Zero(Currency currency) => new(0, currency);

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new CurrencyMismatchException(a.Currency, b.Currency);

        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new CurrencyMismatchException(a.Currency, b.Currency);

        if (a.Amount < b.Amount)
            throw new InsufficientFundsException(b.Amount, a.Amount);

        return new Money(a.Amount - b.Amount, a.Currency);
    }

    public bool IsGreaterThan(Money other)
    {
        if (Currency != other.Currency)
            throw new CurrencyMismatchException(Currency, other.Currency);

        return Amount > other.Amount;
    }

    public override string ToString() => $"{Amount} {Currency}";

    public override bool Equals(object? obj) =>
        obj is Money other && Amount == other.Amount && Currency == other.Currency;

    public override int GetHashCode() => HashCode.Combine(Amount, Currency);
}
