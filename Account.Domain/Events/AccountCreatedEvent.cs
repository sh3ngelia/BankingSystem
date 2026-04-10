using BankingSystem.Shared.Events;

namespace Account.Domain.Events;

public record AccountCreatedEvent(
    Guid AccountId,
    string OwnerId,
    string AccountNumber,
    DateTime OccurredOn) : IDomainEvent;