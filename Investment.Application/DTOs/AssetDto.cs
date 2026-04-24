namespace Investment.Application.DTOs;

public record AssetDto(
    Guid Id,
    string Symbol,
    string Name,
    string Type,
    decimal CurrentPrice,
    DateTime LastUpdatedAt);