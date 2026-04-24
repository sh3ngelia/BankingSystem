using FluentValidation;

namespace Loan.Application.Commands.ApplyForLoan;

public class ApplyForLoanCommandValidator : AbstractValidator<ApplyForLoanCommand>
{
    public ApplyForLoanCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .NotEmpty().WithMessage("ApplicantId is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.")
            .LessThanOrEqualTo(1_000_000).WithMessage("Amount cannot exceed 1,000,000.");

        RuleFor(x => x.InterestRate)
            .GreaterThan(0).WithMessage("Interest rate must be greater than zero.")
            .LessThanOrEqualTo(100).WithMessage("Interest rate cannot exceed 100.");

        RuleFor(x => x.TermMonths)
            .GreaterThan(0).WithMessage("Term must be greater than zero.")
            .LessThanOrEqualTo(360).WithMessage("Term cannot exceed 360 months.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid loan type.");
    }
}