using BankingSystem.Shared.Events;

namespace Identity.Domain.Events;

public record UserLoggedInEvent(
    Guid UserId,
    DateTime OccurredOn) : IDomainEvent;
