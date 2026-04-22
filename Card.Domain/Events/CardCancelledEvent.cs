using BankingSystem.Shared.Events;

namespace Card.Domain.Events;

public record CardCancelledEvent(
    Guid CardId,
    Guid AccountId,
    DateTime OccurredOn) : IDomainEvent;