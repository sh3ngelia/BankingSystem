namespace Transaction.Application.DTOs;

public record TransactionDto(
    Guid Id,
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    string Currency,
    string Type,
    string Status,
    string? FailureReason,
    DateTime CreatedAt,
    DateTime? CompletedAt);