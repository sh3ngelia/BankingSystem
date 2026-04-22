using BankingSystem.Shared.BaseTypes;
using Card.Domain.Enums;
using Card.Domain.Events;
using Card.Domain.Exceptions;
using Card.Domain.ValueObjects;

namespace Card.Domain.Entities;

public class Card : AggregateRoot
{
    public Guid AccountId { get; private set; }
    public CardNumber CardNumber { get; private set; }
    public string CVV { get; private set; }
    public ExpiryDate ExpiryDate { get; private set; }
    public CardType Type { get; private set; }
    public CardStatus Status { get; private set; }
    public decimal DailyLimit { get; private set; }
    public decimal MonthlyLimit { get; private set; }
    public DateTime IssuedAt { get; private set; }

    private readonly List<CardTransaction> _transactions = new();
    public IReadOnlyList<CardTransaction> Transactions => _transactions.AsReadOnly();

    private Card() { }

    public static Card Issue(Guid accountId, CardType type, decimal dailyLimit, decimal monthlyLimit)
    {
        if (dailyLimit <= 0 || monthlyLimit <= 0)
            throw new ArgumentException("Limits must be greater than zero.");

        if (dailyLimit > monthlyLimit)
            throw new ArgumentException("Daily limit cannot exceed monthly limit.");

        var card = new Card
        {
            AccountId = accountId,
            CardNumber = CardNumber.Generate(),
            CVV = GenerateCVV(),
            ExpiryDate = ExpiryDate.CreateDefault(),
            Type = type,
            Status = CardStatus.Active,
            DailyLimit = dailyLimit,
            MonthlyLimit = monthlyLimit,
            IssuedAt = DateTime.UtcNow
        };

        card.AddDomainEvent(new CardIssuedEvent(
            card.Id,
            accountId,
            type.ToString(),
            DateTime.UtcNow));

        return card;
    }

    public void ProcessTransaction(decimal amount, string currency, string merchantName, string merchantCategory)
    {
        ValidateCardForTransaction();
        ValidateDailyLimit(amount);

        var transaction = CardTransaction.Create(
            Id,
            amount,
            currency,
            merchantName,
            merchantCategory);

        _transactions.Add(transaction);

        AddDomainEvent(new CardTransactionProcessedEvent(
            Id,
            transaction.Id,
            amount,
            merchantName,
            DateTime.UtcNow));
    }

    public void Freeze()
    {
        if (Status == CardStatus.Cancelled)
            throw new CardCancelledException(Id);

        if (Status == CardStatus.Expired)
            throw new CardExpiredException(Id);

        Status = CardStatus.Frozen;

        AddDomainEvent(new CardFrozenEvent(Id, AccountId, DateTime.UtcNow));
    }

    public void Unfreeze()
    {
        if (Status != CardStatus.Frozen)
            throw new InvalidOperationException("Card is not frozen.");

        Status = CardStatus.Active;

        AddDomainEvent(new CardUnfrozenEvent(Id, AccountId, DateTime.UtcNow));
    }

    public void Cancel()
    {
        if (Status == CardStatus.Cancelled)
            throw new InvalidOperationException("Card is already cancelled.");

        Status = CardStatus.Cancelled;

        AddDomainEvent(new CardCancelledEvent(Id, AccountId, DateTime.UtcNow));
    }

    public void UpdateLimits(decimal dailyLimit, decimal monthlyLimit)
    {
        if (dailyLimit <= 0 || monthlyLimit <= 0)
            throw new ArgumentException("Limits must be greater than zero.");

        if (dailyLimit > monthlyLimit)
            throw new ArgumentException("Daily limit cannot exceed monthly limit.");

        DailyLimit = dailyLimit;
        MonthlyLimit = monthlyLimit;
    }

    private void ValidateCardForTransaction()
    {
        if (Status == CardStatus.Frozen)
            throw new CardFrozenException(Id);

        if (Status == CardStatus.Cancelled)
            throw new CardCancelledException(Id);

        if (Status == CardStatus.Expired || ExpiryDate.IsExpired)
            throw new CardExpiredException(Id);
    }

    private void ValidateDailyLimit(decimal amount)
    {
        var todayTotal = _transactions
            .Where(t => t.CreatedAt.Date == DateTime.UtcNow.Date)
            .Sum(t => t.Amount);

        if (todayTotal + amount > DailyLimit)
            throw new InvalidOperationException(
                $"Daily limit of {DailyLimit} exceeded.");
    }

    private static string GenerateCVV()
    {
        var random = new Random();
        return random.Next(100, 999).ToString();
    }
}