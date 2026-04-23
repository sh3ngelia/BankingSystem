using Account.Domain.Enums;
using FluentValidation;

namespace Account.Application.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("OwnerId is required.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid account type.");

        RuleFor(x => x.Currency)
            .IsInEnum().WithMessage("Invalid currency.");
    }
}