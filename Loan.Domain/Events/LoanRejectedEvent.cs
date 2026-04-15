using BankingSystem.Shared.Events;

namespace Loan.Domain.Events;

public record LoanRejectedEvent(
    Guid LoanId,
    Guid ApplicantId,
    string Reason,
    DateTime OccurredOn) : IDomainEvent;