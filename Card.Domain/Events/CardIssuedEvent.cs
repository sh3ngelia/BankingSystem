using BankingSystem.Shared.Events;

namespace Card.Domain.Events;

public record CardIssuedEvent(
    Guid CardId,
    Guid AccountId,
    string CardType,
    DateTime OccurredOn) : IDomainEvent;