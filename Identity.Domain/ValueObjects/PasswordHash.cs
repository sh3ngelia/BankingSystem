using BankingSystem.Shared.Abstractions;

namespace Identity.Domain.ValueObjects;

public sealed class PasswordHash : IValueObject
{
    public string Value { get; }
    private PasswordHash(string value)
    {
        Value = value;
    }

    public static PasswordHash Create(string hashedValue)
    {
        if(string.IsNullOrEmpty(hashedValue))
            throw new ArgumentException("Password hash cannot be empty.");

        return new PasswordHash(hashedValue);
    }

    public override string ToString() => Value;
}
