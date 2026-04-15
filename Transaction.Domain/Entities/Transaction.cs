using BankingSystem.Shared.BaseTypes;
using Transaction.Domain.Enums;
using Transaction.Domain.Events;
using Transaction.Domain.Exceptions;

namespace Transaction.Domain.Entities;

public class Transaction : AggregateRoot
{
    public Guid FromAccountId { get; private set; }
    public Guid ToAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public TransactionType Type { get; private set; }
    public TransactionStatus Status { get; private set; }
    public string? FailureReason { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private Transaction() { }

    public static Transaction Initiate(
        Guid fromAccountId,
        Guid toAccountId,
        decimal amount,
        string currency,
        TransactionType type)
    {
        if (amount <= 0)
            throw new InvalidTransactionException("Amount must be greater than zero.");

        if (fromAccountId == toAccountId)
            throw new InvalidTransactionException("Cannot transfer to the same account.");

        var transaction = new Transaction
        {
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Amount = amount,
            Currency = currency,
            Type = type,
            Status = TransactionStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        transaction.AddDomainEvent(new TransactionInitiatedEvent(
            transaction.Id,
            fromAccountId,
            toAccountId,
            amount,
            currency,
            DateTime.UtcNow));

        return transaction;
    }

    public void Complete()
    {
        if (Status != TransactionStatus.Pending)
            throw new InvalidTransactionException("Only pending transactions can be completed.");

        Status = TransactionStatus.Completed;
        CompletedAt = DateTime.UtcNow;

        AddDomainEvent(new TransactionCompletedEvent(
            Id,
            FromAccountId,
            ToAccountId,
            Amount,
            DateTime.UtcNow));
    }

    public void Fail(string reason)
    {
        if (Status != TransactionStatus.Pending)
            throw new InvalidTransactionException("Only pending transactions can be failed.");

        Status = TransactionStatus.Failed;
        FailureReason = reason;

        AddDomainEvent(new TransactionFailedEvent(Id, reason, DateTime.UtcNow));
    }

    public void Reverse(string reason)
    {
        if (Status != TransactionStatus.Completed)
            throw new InvalidTransactionException("Only completed transactions can be reversed.");

        Status = TransactionStatus.Reversed;
        FailureReason = reason;

        AddDomainEvent(new TransactionReversedEvent(Id, reason, DateTime.UtcNow));
    }
}