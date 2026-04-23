using BankingSystem.Shared.Events;

namespace Investment.Domain.Events;

public record AssetSoldEvent(
    Guid PortfolioId,
    Guid AssetId,
    string Symbol,
    decimal Quantity,
    decimal PricePerUnit,
    decimal RealizedProfitLoss,
    DateTime OccurredOn) : IDomainEvent;