using FluentValidation;

namespace Card.Application.Commands.IssueCard;

public class IssueCardCommandValidator : AbstractValidator<IssueCardCommand>
{
    public IssueCardCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid card type.");

        RuleFor(x => x.DailyLimit)
            .GreaterThan(0).WithMessage("Daily limit must be greater than zero.");

        RuleFor(x => x.MonthlyLimit)
            .GreaterThan(0).WithMessage("Monthly limit must be greater than zero.")
            .GreaterThanOrEqualTo(x => x.DailyLimit)
            .WithMessage("Monthly limit must be greater than or equal to daily limit.");
    }
}