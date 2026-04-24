using Investment.Application.Abstractions;

namespace Investment.Application.Commands.BuyAsset;

public record BuyAssetCommand(
    Guid PortfolioId,
    Guid AssetId,
    decimal Quantity) : ICommand;