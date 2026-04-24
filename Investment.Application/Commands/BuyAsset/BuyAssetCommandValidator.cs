using FluentValidation;

namespace Investment.Application.Commands.BuyAsset;

public class BuyAssetCommandValidator : AbstractValidator<BuyAssetCommand>
{
    public BuyAssetCommandValidator()
    {
        RuleFor(x => x.PortfolioId)
            .NotEmpty().WithMessage("PortfolioId is required.");

        RuleFor(x => x.AssetId)
            .NotEmpty().WithMessage("AssetId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}