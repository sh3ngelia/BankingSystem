using BankingSystem.Shared.BaseTypes;
using Transaction.Domain.Exceptions;

namespace Transaction.Domain.Entities;

public class TransactionLimit : Entity
{
    public Guid AccountId { get; private set; }
    public decimal DailyLimit { get; private set; }
    public decimal MonthlyLimit { get; private set; }
    public decimal DailyUsed { get; private set; }
    public decimal MonthlyUsed { get; private set; }
    public DateTime LastResetDate { get; private set; }

    private TransactionLimit() { }

    public static TransactionLimit Create(Guid accountId, decimal dailyLimit, decimal monthlyLimit)
    {
        if (dailyLimit <= 0 || monthlyLimit <= 0)
            throw new ArgumentException("Limits must be greater than zero.");

        if (dailyLimit > monthlyLimit)
            throw new ArgumentException("Daily limit cannot exceed monthly limit.");

        return new TransactionLimit
        {
            AccountId = accountId,
            DailyLimit = dailyLimit,
            MonthlyLimit = monthlyLimit,
            DailyUsed = 0,
            MonthlyUsed = 0,
            LastResetDate = DateTime.UtcNow.Date
        };
    }

    public void ValidateAndRegister(decimal amount)
    {
        ResetIfNeeded();

        if (DailyUsed + amount > DailyLimit)
            throw new TransactionLimitExceededException(DailyLimit, "Daily");

        if (MonthlyUsed + amount > MonthlyLimit)
            throw new TransactionLimitExceededException(MonthlyLimit, "Monthly");

        DailyUsed += amount;
        MonthlyUsed += amount;
    }

    private void ResetIfNeeded()
    {
        var today = DateTime.UtcNow.Date;

        if (LastResetDate.Month != today.Month)
        {
            DailyUsed = 0;
            MonthlyUsed = 0;
            LastResetDate = today;
            return;
        }

        if (LastResetDate != today)
        {
            DailyUsed = 0;
            LastResetDate = today;
        }
    }
}