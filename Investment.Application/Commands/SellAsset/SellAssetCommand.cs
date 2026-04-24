using Investment.Application.Abstractions;

namespace Investment.Application.Commands.SellAsset;

public record SellAssetCommand(
    Guid PortfolioId,
    Guid AssetId,
    decimal Quantity) : ICommand;