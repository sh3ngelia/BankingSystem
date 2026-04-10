using BankingSystem.Shared.Abstractions;

namespace Account.Domain.ValueObjects;

public sealed class AccountNumber : IValueObject
{
    public string Value { get; }

    private AccountNumber(string value)
    {
        Value = value;
    }

    public static AccountNumber Generate()
    {
        var random = new Random();
        var number = $"GE{random.Next(10, 99)}BS{random.Next(1000000000, int.MaxValue)}";
        return new AccountNumber(number);
    }

    public static AccountNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Account number cannot be empty.");

        return new AccountNumber(value);
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj) =>
        obj is AccountNumber other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}