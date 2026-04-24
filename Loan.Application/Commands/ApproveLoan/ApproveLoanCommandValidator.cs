using FluentValidation;

namespace Loan.Application.Commands.ApproveLoan;

public class ApproveLoanCommandValidator : AbstractValidator<ApproveLoanCommand>
{
    public ApproveLoanCommandValidator()
    {
        RuleFor(x => x.LoanId)
            .NotEmpty().WithMessage("LoanId is required.");

        RuleFor(x => x.CreditScore)
            .InclusiveBetween(300, 850).WithMessage("Credit score must be between 300 and 850.");
    }
}