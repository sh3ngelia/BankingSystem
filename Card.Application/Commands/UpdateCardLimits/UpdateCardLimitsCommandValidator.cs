using FluentValidation;

namespace Card.Application.Commands.UpdateCardLimits;

public class UpdateCardLimitsCommandValidator : AbstractValidator<UpdateCardLimitsCommand>
{
    public UpdateCardLimitsCommandValidator()
    {
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("CardId is required.");

        RuleFor(x => x.DailyLimit)
            .GreaterThan(0).WithMessage("Daily limit must be greater than zero.");

        RuleFor(x => x.MonthlyLimit)
            .GreaterThan(0).WithMessage("Monthly limit must be greater than zero.")
            .GreaterThanOrEqualTo(x => x.DailyLimit)
            .WithMessage("Monthly limit must be greater than or equal to daily limit.");
    }
}