using BankingSystem.Shared.BaseTypes;

namespace Card.Domain.Entities;

public class CardTransaction : Entity
{
    public Guid CardId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public string MerchantName { get; private set; }
    public string MerchantCategory { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private CardTransaction() { }

    internal static CardTransaction Create(
        Guid cardId,
        decimal amount,
        string currency,
        string merchantName,
        string merchantCategory)
    {
        if (amount <= 0)
            throw new ArgumentException("Transaction amount must be greater than zero.");

        return new CardTransaction
        {
            CardId = cardId,
            Amount = amount,
            Currency = currency,
            MerchantName = merchantName,
            MerchantCategory = merchantCategory,
            CreatedAt = DateTime.UtcNow
        };
    }
}