namespace Card.Application.DTOs;

public record CardTransactionDto(
    Guid Id,
    Guid CardId,
    decimal Amount,
    string Currency,
    string MerchantName,
    string MerchantCategory,
    DateTime CreatedAt);