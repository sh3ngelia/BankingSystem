using BankingSystem.Shared.Events;

namespace Investment.Domain.Events;

public record AssetPurchasedEvent(
    Guid PortfolioId,
    Guid AssetId,
    string Symbol,
    decimal Quantity,
    decimal PricePerUnit,
    DateTime OccurredOn) : IDomainEvent;