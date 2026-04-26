using Dapper;
using Investment.Application.Abstractions;
using Investment.Domain.Entities;
using Investment.Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Investment.Infrastructure.Persistence.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly InvestmentDbContext _context;
    private readonly string _connectionString;

    public AssetRepository(InvestmentDbContext context, string connectionString)
    {
        _context = context;
        _connectionString = connectionString;
    }

    public async Task<Asset?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Assets
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken ct = default)
    {
        return await _context.Assets
            .FirstOrDefaultAsync(a => a.Symbol == symbol.ToUpperInvariant(), ct);
    }

    public async Task<IEnumerable<Asset>> GetAllAsync(CancellationToken ct = default)
    {
        using var connection = new SqlConnection(_connectionString);

        const string sql = """
            SELECT Id, Symbol, Name, Type, CurrentPrice, LastUpdatedAt
            FROM Assets
            ORDER BY Symbol ASC
            """;

        var results = await connection.QueryAsync<AssetQueryResult>(sql);

        return results.Select(r => Asset.Create(
            r.Symbol,
            r.Name,
            Enum.Parse<AssetType>(r.Type),
            r.CurrentPrice));
    }

    public async Task AddAsync(Asset asset, CancellationToken ct = default)
    {
        await _context.Assets.AddAsync(asset, ct);
    }

    public async Task UpdateAsync(Asset asset, CancellationToken ct = default)
    {
        _context.Assets.Update(asset);
        await Task.CompletedTask;
    }

    private record AssetQueryResult(
        Guid Id,
        string Symbol,
        string Name,
        string Type,
        decimal CurrentPrice,
        DateTime LastUpdatedAt);
}