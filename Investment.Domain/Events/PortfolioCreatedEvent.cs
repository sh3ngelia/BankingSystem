using BankingSystem.Shared.Events;

namespace Investment.Domain.Events;

public record PortfolioCreatedEvent(
    Guid PortfolioId,
    Guid OwnerId,
    string Name,
    DateTime OccurredOn) : IDomainEvent;