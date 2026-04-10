using BankingSystem.Shared.Abstractions;
using System.Runtime.InteropServices;

namespace Identity.Domain.ValueObjects;

public sealed class Email : IValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value) 
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.");
        if(!value.Contains("@"))
            throw new ArgumentException("Email is not valid.");

        return new Email(value.ToLowerInvariant());
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj) => 
        obj is Email other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
