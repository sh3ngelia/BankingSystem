using BankingSystem.Shared.BaseTypes;
using Investment.Domain.Exceptions;
using Investment.Domain.ValueObjects;

namespace Investment.Domain.Entities;

public class Position : Entity
{
    public Guid PortfolioId { get; private set; }
    public Guid AssetId { get; private set; }
    public string Symbol { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal AverageCost { get; private set; }

    public decimal TotalCost => Quantity * AverageCost;

    public decimal CurrentValue(decimal currentPrice) => Quantity * currentPrice;

    public ProfitLoss GetProfitLoss(decimal currentPrice) =>
        ProfitLoss.Calculate(CurrentValue(currentPrice), TotalCost);

    private Position() { }

    internal static Position Open(Guid portfolioId, Guid assetId, string symbol, decimal quantity, decimal pricePerUnit)
    {
        return new Position
        {
            PortfolioId = portfolioId,
            AssetId = assetId,
            Symbol = symbol,
            Quantity = quantity,
            AverageCost = pricePerUnit
        };
    }

    internal void AddShares(decimal quantity, decimal pricePerUnit)
    {
        var totalCost = TotalCost + (quantity * pricePerUnit);
        var totalQuantity = Quantity + quantity;

        AverageCost = totalCost / totalQuantity;
        Quantity = totalQuantity;
    }

    internal decimal RemoveShares(decimal quantity)
    {
        if (quantity > Quantity)
            throw new InsufficientSharesException(Symbol, quantity, Quantity);

        Quantity -= quantity;
        return quantity * AverageCost;
    }

    public bool IsClosed => Quantity == 0;
}