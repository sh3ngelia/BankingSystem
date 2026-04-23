using FluentValidation;

namespace Account.Application.Commands.DepositMoney;

public class DepositMoneyCommandValidator : AbstractValidator<DepositMoneyCommand>
{
    public DepositMoneyCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.");
    }
}