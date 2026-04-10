using BankingSystem.Shared.Events;

namespace Account.Domain.Events;

public record MoneyDepositedEvent(
    Guid AccountId,
    decimal Amount,
    string Currency,
    DateTime OccurredOn) : IDomainEvent;