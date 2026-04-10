using Account.Domain.Enums;

namespace Account.Domain.Exceptions;

public class CurrencyMismatchException : Exception
{
    public CurrencyMismatchException(Currency a, Currency b)
        : base($"Currency mismatch: '{a}' and '{b}'.") { }
}