using BankingSystem.Shared.Events;

namespace Loan.Domain.Events;

public record LoanApprovedEvent(
    Guid LoanId,
    Guid ApplicantId,
    decimal Amount,
    DateTime OccurredOn) : IDomainEvent;