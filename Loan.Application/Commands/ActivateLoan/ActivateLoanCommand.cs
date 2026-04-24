using Loan.Application.Abstractions;

namespace Loan.Application.Commands.ActivateLoan;

public record ActivateLoanCommand(Guid LoanId) : ICommand;