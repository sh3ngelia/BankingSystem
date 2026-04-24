using Investment.Domain.Entities;

namespace Investment.Application.Abstractions;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken ct = default);
    Task<IEnumerable<Asset>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Asset asset, CancellationToken ct = default);
    Task UpdateAsync(Asset asset, CancellationToken ct = default);
}