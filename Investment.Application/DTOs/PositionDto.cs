namespace Investment.Application.DTOs;

public record PositionDto(
    Guid Id,
    Guid AssetId,
    string Symbol,
    decimal Quantity,
    decimal AverageCost,
    decimal TotalCost,
    decimal CurrentValue,
    decimal ProfitLossAmount,
    decimal ProfitLossPercentage,
    bool IsProfit);