using BankingSystem.Shared.Events;

namespace Card.Domain.Events;

public record CardUnfrozenEvent(
    Guid CardId,
    Guid AccountId,
    DateTime OccurredOn) : IDomainEvent;