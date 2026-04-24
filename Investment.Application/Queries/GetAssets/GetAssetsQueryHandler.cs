using Investment.Application.Abstractions;
using Investment.Application.DTOs;

namespace Investment.Application.Queries.GetAssets;

public class GetAssetsQueryHandler : IQueryHandler<GetAssetsQuery, IEnumerable<AssetDto>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAssetsQueryHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<IEnumerable<AssetDto>> Handle(GetAssetsQuery query, CancellationToken ct)
    {
        var assets = await _assetRepository.GetAllAsync(ct);

        return assets.Select(a => new AssetDto(
            a.Id,
            a.Symbol,
            a.Name,
            a.Type.ToString(),
            a.CurrentPrice,
            a.LastUpdatedAt));
    }
}