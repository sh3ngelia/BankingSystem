using FluentValidation;

namespace Transaction.Application.Commands.InitiateTransaction;

public class InitiateTransactionCommandValidator : AbstractValidator<InitiateTransactionCommand>
{
    public InitiateTransactionCommandValidator()
    {
        RuleFor(x => x.FromAccountId)
            .NotEmpty().WithMessage("FromAccountId is required.");

        RuleFor(x => x.ToAccountId)
            .NotEmpty().WithMessage("ToAccountId is required.");

        RuleFor(x => x.ToAccountId)
            .NotEqual(x => x.FromAccountId)
            .WithMessage("Cannot transfer to the same account.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid transaction type.");
    }
}