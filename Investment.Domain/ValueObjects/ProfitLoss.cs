using BankingSystem.Shared.Abstractions;

namespace Investment.Domain.ValueObjects;

public sealed class ProfitLoss : IValueObject
{
    public decimal Amount { get; }
    public decimal Percentage { get; }
    public bool IsProfit => Amount >= 0;

    private ProfitLoss(decimal amount, decimal percentage)
    {
        Amount = amount;
        Percentage = percentage;
    }

    public static ProfitLoss Calculate(decimal currentValue, decimal costBasis)
    {
        if (costBasis == 0)
            throw new ArgumentException("Cost basis cannot be zero.");

        var amount = currentValue - costBasis;
        var percentage = (amount / costBasis) * 100;

        return new ProfitLoss(
            Math.Round(amount, 2),
            Math.Round(percentage, 2));
    }

    public override string ToString() =>
        $"{(IsProfit ? "+" : "")}{Amount} ({(IsProfit ? "+" : "")}{Percentage}%)";

    public override bool Equals(object? obj) =>
        obj is ProfitLoss other &&
        Amount == other.Amount &&
        Percentage == other.Percentage;

    public override int GetHashCode() => HashCode.Combine(Amount, Percentage);
}