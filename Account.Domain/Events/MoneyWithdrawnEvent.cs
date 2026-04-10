using BankingSystem.Shared.Events;

namespace Account.Domain.Events;

public record MoneyWithdrawnEvent(
    Guid AccountId,
    decimal Amount,
    string Currency,
    DateTime OccurredOn) : IDomainEvent;