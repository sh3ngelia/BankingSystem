namespace Investment.Application.DTOs;

public record PortfolioDto(
    Guid Id,
    Guid OwnerId,
    string Name,
    decimal TotalInvested,
    decimal TotalCurrentValue,
    decimal TotalProfitLossAmount,
    decimal TotalProfitLossPercentage,
    DateTime CreatedAt,
    IEnumerable<PositionDto> Positions);