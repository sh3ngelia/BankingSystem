using BankingSystem.Shared.Abstractions;

namespace Loan.Domain.ValueObjects;

public sealed class InterestRate : IValueObject
{
    public decimal Value { get; }

    private InterestRate(decimal value)
    {
        Value = value;
    }

    public static InterestRate Create(decimal value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentException("Interest rate must be between 0 and 100.");

        return new InterestRate(value);
    }

    public decimal AsDecimalFraction() => Value / 100;

    public override string ToString() => $"{Value}%";

    public override bool Equals(object? obj) =>
        obj is InterestRate other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}