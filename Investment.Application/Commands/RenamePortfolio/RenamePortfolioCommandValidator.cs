using FluentValidation;

namespace Investment.Application.Commands.RenamePortfolio;

public class RenamePortfolioCommandValidator : AbstractValidator<RenamePortfolioCommand>
{
    public RenamePortfolioCommandValidator()
    {
        RuleFor(x => x.PortfolioId)
            .NotEmpty().WithMessage("PortfolioId is required.");

        RuleFor(x => x.NewName)
            .NotEmpty().WithMessage("New name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}