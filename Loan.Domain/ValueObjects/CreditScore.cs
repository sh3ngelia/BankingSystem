using BankingSystem.Shared.Abstractions;

namespace Loan.Domain.ValueObjects;

public sealed class CreditScore : IValueObject
{
    public int Value { get; }

    public bool IsExcellent => Value >= 750;
    public bool IsGood => Value >= 670 && Value < 750;
    public bool IsFair => Value >= 580 && Value < 670;
    public bool IsPoor => Value < 580;

    private CreditScore(int value)
    {
        Value = value;
    }

    public static CreditScore Create(int value)
    {
        if (value < 300 || value > 850)
            throw new ArgumentException("Credit score must be between 300 and 850.");

        return new CreditScore(value);
    }

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) =>
        obj is CreditScore other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}