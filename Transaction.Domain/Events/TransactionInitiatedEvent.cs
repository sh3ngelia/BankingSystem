using BankingSystem.Shared.Events;

namespace Transaction.Domain.Events;

public record TransactionInitiatedEvent(
    Guid TransactionId,
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    string Currency,
    DateTime OccurredOn) : IDomainEvent;