using BankingSystem.Shared.Events;

namespace Loan.Domain.Events;

public record LoanAppliedEvent(
    Guid LoanId,
    Guid ApplicantId,
    decimal Amount,
    DateTime OccurredOn) : IDomainEvent;