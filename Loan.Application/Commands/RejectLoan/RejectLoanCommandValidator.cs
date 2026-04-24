using FluentValidation;

namespace Loan.Application.Commands.RejectLoan;

public class RejectLoanCommandValidator : AbstractValidator<RejectLoanCommand>
{
    public RejectLoanCommandValidator()
    {
        RuleFor(x => x.LoanId)
            .NotEmpty().WithMessage("LoanId is required.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Rejection reason is required.")
            .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters.");
    }
}