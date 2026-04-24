using Investment.Application.Abstractions;
using Investment.Application.DTOs;

namespace Investment.Application.Queries.GetAssets;

public record GetAssetsQuery : IQuery<IEnumerable<AssetDto>>;