using Loan.Application.Abstractions;

namespace Loan.Application.Commands.PayInstallment;

public record PayInstallmentCommand(Guid LoanId) : ICommand;