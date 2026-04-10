using BankingSystem.Shared.Events;

namespace Account.Domain.Events;

public record AccountFrozenEvent(
    Guid AccountId,
    DateTime OccurredOn) : IDomainEvent;