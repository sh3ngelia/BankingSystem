using FluentValidation;

namespace Investment.Application.Commands.CreatePortfolio;

public class CreatePortfolioCommandValidator : AbstractValidator<CreatePortfolioCommand>
{
    public CreatePortfolioCommandValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("OwnerId is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Portfolio name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}