using BankingSystem.Shared.Results;
using Investment.Application.Abstractions;
using Investment.Domain.Events;

namespace Investment.Application.Commands.SellAsset;

public class SellAssetCommandHandler : ICommandHandler<SellAssetCommand>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public SellAssetCommandHandler(
        IPortfolioRepository portfolioRepository,
        IAssetRepository assetRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _portfolioRepository = portfolioRepository;
        _assetRepository = assetRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SellAssetCommand command, CancellationToken ct)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(command.PortfolioId, ct);
        if (portfolio is null)
            return Result.Failure("Portfolio not found.");

        var asset = await _assetRepository.GetByIdAsync(command.AssetId, ct);
        if (asset is null)
            return Result.Failure("Asset not found.");

        portfolio.Sell(asset, command.Quantity);

        await _portfolioRepository.UpdateAsync(portfolio, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        var position = portfolio.Positions
            .FirstOrDefault(p => p.AssetId == asset.Id);

        var realizedPnL = (asset.CurrentPrice - (position?.AverageCost ?? 0)) * command.Quantity;

        await _eventBus.PublishAsync(new AssetSoldEvent(
            portfolio.Id,
            asset.Id,
            asset.Symbol,
            command.Quantity,
            asset.CurrentPrice,
            Math.Round(realizedPnL, 2),
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}