namespace Account.Application.DTOs;

public record AccountDto(
    Guid Id,
    string AccountNumber,
    Guid OwnerId,
    decimal Balance,
    string Currency,
    string Type,
    string Status,
    DateTime CreatedAt);