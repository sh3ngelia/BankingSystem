using BankingSystem.Shared.Events;

namespace Loan.Domain.Events;

public record LoanClosedEvent(
    Guid LoanId,
    DateTime OccurredOn) : IDomainEvent;