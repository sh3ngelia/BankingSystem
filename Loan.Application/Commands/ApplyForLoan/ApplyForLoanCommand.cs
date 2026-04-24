using Loan.Application.Abstractions;
using Loan.Domain.Enums;

namespace Loan.Application.Commands.ApplyForLoan;

public record ApplyForLoanCommand(
    Guid ApplicantId,
    decimal Amount,
    decimal InterestRate,
    int TermMonths,
    LoanType Type) : ICommand<Guid>;