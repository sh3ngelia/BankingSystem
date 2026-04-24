using Loan.Application.Abstractions;

namespace Loan.Application.Commands.ApproveLoan;

public record ApproveLoanCommand(
    Guid LoanId,
    int CreditScore) : ICommand;