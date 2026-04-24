namespace Card.Application.DTOs;

public record CardDto(
    Guid Id,
    Guid AccountId,
    string MaskedCardNumber,
    string ExpiryDate,
    string Type,
    string Status,
    decimal DailyLimit,
    decimal MonthlyLimit,
    DateTime IssuedAt);