using BankingSystem.Shared.Events;

namespace Card.Domain.Events;

public record CardFrozenEvent(
    Guid CardId,
    Guid AccountId,
    DateTime OccurredOn) : IDomainEvent;