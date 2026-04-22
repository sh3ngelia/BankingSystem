using BankingSystem.Shared.Events;

namespace Card.Domain.Events;

public record CardTransactionProcessedEvent(
    Guid CardId,
    Guid TransactionId,
    decimal Amount,
    string MerchantName,
    DateTime OccurredOn) : IDomainEvent;