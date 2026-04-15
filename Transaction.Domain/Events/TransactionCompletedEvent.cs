using BankingSystem.Shared.Events;

namespace Transaction.Domain.Events;

public record TransactionCompletedEvent(
    Guid TransactionId,
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    DateTime OccurredOn) : IDomainEvent;