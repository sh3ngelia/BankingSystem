using BankingSystem.Shared.BaseTypes;
using Investment.Domain.Events;
using Investment.Domain.Exceptions;

namespace Investment.Domain.Entities;

public class Portfolio : AggregateRoot
{
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<Position> _positions = new();
    public IReadOnlyList<Position> Positions => _positions.AsReadOnly();

    public decimal TotalInvested => _positions.Sum(p => p.TotalCost);

    public decimal TotalCurrentValue(Func<Guid, decimal> getCurrentPrice) =>
        _positions.Sum(p => p.CurrentValue(getCurrentPrice(p.AssetId)));

    private Portfolio() { }

    public static Portfolio Create(Guid ownerId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Portfolio name cannot be empty.");

        var portfolio = new Portfolio
        {
            OwnerId = ownerId,
            Name = name,
            CreatedAt = DateTime.UtcNow
        };

        portfolio.AddDomainEvent(new PortfolioCreatedEvent(
            portfolio.Id,
            ownerId,
            name,
            DateTime.UtcNow));

        return portfolio;
    }

    public void Buy(Asset asset, decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        var existingPosition = _positions
            .FirstOrDefault(p => p.AssetId == asset.Id);

        if (existingPosition != null)
        {
            existingPosition.AddShares(quantity, asset.CurrentPrice);
        }
        else
        {
            var position = Position.Open(
                Id,
                asset.Id,
                asset.Symbol,
                quantity,
                asset.CurrentPrice);

            _positions.Add(position);
        }

        AddDomainEvent(new AssetPurchasedEvent(
            Id,
            asset.Id,
            asset.Symbol,
            quantity,
            asset.CurrentPrice,
            DateTime.UtcNow));
    }

    public void Sell(Asset asset, decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        var position = _positions.FirstOrDefault(p => p.AssetId == asset.Id)
            ?? throw new AssetNotFoundException(asset.Symbol);

        var costBasis = position.RemoveShares(quantity);
        var saleValue = quantity * asset.CurrentPrice;
        var realizedPnL = saleValue - costBasis;

        if (position.IsClosed)
            _positions.Remove(position);

        AddDomainEvent(new AssetSoldEvent(
            Id,
            asset.Id,
            asset.Symbol,
            quantity,
            asset.CurrentPrice,
            Math.Round(realizedPnL, 2),
            DateTime.UtcNow));
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Portfolio name cannot be empty.");

        Name = newName;
    }
}