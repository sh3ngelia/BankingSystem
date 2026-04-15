using BankingSystem.Shared.Events;

namespace Transaction.Domain.Events;

public record TransactionFailedEvent(
    Guid TransactionId,
    string Reason,
    DateTime OccurredOn) : IDomainEvent;