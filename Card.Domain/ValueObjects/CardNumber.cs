using BankingSystem.Shared.Abstractions;

namespace Card.Domain.ValueObjects;

public sealed class CardNumber : IValueObject
{
    public string Value { get; }
    public string Masked => $"**** **** **** {Value[^4..]}";

    private CardNumber(string value)
    {
        Value = value;
    }

    public static CardNumber Generate()
    {
        var random = new Random();
        var number = string.Join("",
            Enumerable.Range(0, 16).Select(_ => random.Next(0, 10)));
        return new CardNumber(number);
    }

    public static CardNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 16)
            throw new ArgumentException("Card number must be 16 digits.");

        if (!value.All(char.IsDigit))
            throw new ArgumentException("Card number must contain only digits.");

        return new CardNumber(value);
    }

    public override string ToString() => Masked;

    public override bool Equals(object? obj) =>
        obj is CardNumber other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}