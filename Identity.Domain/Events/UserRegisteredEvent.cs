using BankingSystem.Shared.Events;

namespace Identity.Domain.Events;

public record UserRegisteredEvent(
    Guid UserId,
    string Email,
    DateTime OccurredOn) : IDomainEvent;