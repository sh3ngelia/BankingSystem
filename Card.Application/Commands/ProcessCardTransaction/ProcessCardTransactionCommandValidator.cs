using FluentValidation;

namespace Card.Application.Commands.ProcessCardTransaction;

public class ProcessCardTransactionCommandValidator : AbstractValidator<ProcessCardTransactionCommand>
{
    public ProcessCardTransactionCommandValidator()
    {
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("CardId is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.");

        RuleFor(x => x.MerchantName)
            .NotEmpty().WithMessage("Merchant name is required.")
            .MaximumLength(100).WithMessage("Merchant name cannot exceed 100 characters.");

        RuleFor(x => x.MerchantCategory)
            .NotEmpty().WithMessage("Merchant category is required.");
    }
}