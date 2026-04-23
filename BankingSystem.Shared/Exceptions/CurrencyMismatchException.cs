using BankingSystem.Shared.Enums;

namespace BankingSystem.Shared.Exceptions;

public class CurrencyMismatchException : Exception
{
    public CurrencyMismatchException(Currency a, Currency b)
        : base($"Currency mismatch: '{a}' and '{b}'.") { }
}
