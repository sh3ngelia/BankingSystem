using Loan.Application.Abstractions;

namespace Loan.Application.Commands.RejectLoan;

public record RejectLoanCommand(
    Guid LoanId,
    string Reason) : ICommand;