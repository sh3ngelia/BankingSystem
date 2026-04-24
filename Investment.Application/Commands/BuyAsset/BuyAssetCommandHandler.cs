using BankingSystem.Shared.Results;
using Investment.Application.Abstractions;
using Investment.Domain.Events;

namespace Investment.Application.Commands.BuyAsset;

public class BuyAssetCommandHandler : ICommandHandler<BuyAssetCommand>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public BuyAssetCommandHandler(
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

    public async Task<Result> Handle(BuyAssetCommand command, CancellationToken ct)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(command.PortfolioId, ct);
        if (portfolio is null)
            return Result.Failure("Portfolio not found.");

        var asset = await _assetRepository.GetByIdAsync(command.AssetId, ct);
        if (asset is null)
            return Result.Failure("Asset not found.");

        portfolio.Buy(asset, command.Quantity);

        await _portfolioRepository.UpdateAsync(portfolio, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new AssetPurchasedEvent(
            portfolio.Id,
            asset.Id,
            asset.Symbol,
            command.Quantity,
            asset.CurrentPrice,
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}