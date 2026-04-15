using BankingSystem.Shared.Events;

namespace Transaction.Domain.Events;

public record TransactionReversedEvent(
    Guid TransactionId,
    string Reason,
    DateTime OccurredOn) : IDomainEvent;