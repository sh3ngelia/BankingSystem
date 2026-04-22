using BankingSystem.Shared.Abstractions;

namespace Card.Domain.ValueObjects;

public sealed class ExpiryDate : IValueObject
{
    public int Month { get; }
    public int Year { get; }

    public bool IsExpired => new DateTime(Year, Month, 1)
        .AddMonths(1) <= DateTime.UtcNow;

    private ExpiryDate(int month, int year)
    {
        Month = month;
        Year = year;
    }

    public static ExpiryDate Create(int month, int year)
    {
        if (month < 1 || month > 12)
            throw new ArgumentException("Month must be between 1 and 12.");

        if (year < DateTime.UtcNow.Year)
            throw new ArgumentException("Year cannot be in the past.");

        return new ExpiryDate(month, year);
    }

    public static ExpiryDate CreateDefault() =>
        Create(DateTime.UtcNow.Month, DateTime.UtcNow.Year + 3);

    public override string ToString() =>
        $"{Month:D2}/{Year % 100:D2}";

    public override bool Equals(object? obj) =>
        obj is ExpiryDate other && Month == other.Month && Year == other.Year;

    public override int GetHashCode() => HashCode.Combine(Month, Year);
}