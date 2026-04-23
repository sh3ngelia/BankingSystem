using BankingSystem.Shared.BaseTypes;
using Investment.Domain.Enums;

namespace Investment.Domain.Entities;

public class Asset : Entity
{
    public string Symbol { get; private set; }
    public string Name { get; private set; }
    public AssetType Type { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }

    private Asset() { }

    public static Asset Create(string symbol, string name, AssetType type, decimal currentPrice)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be empty.");

        if (currentPrice <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        return new Asset
        {
            Symbol = symbol.ToUpperInvariant(),
            Name = name,
            Type = type,
            CurrentPrice = currentPrice,
            LastUpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        CurrentPrice = newPrice;
        LastUpdatedAt = DateTime.UtcNow;
    }
}