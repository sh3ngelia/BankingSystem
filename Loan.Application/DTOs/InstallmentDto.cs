namespace Loan.Application.DTOs;

public record InstallmentDto(
    Guid Id,
    int InstallmentNumber,
    decimal Amount,
    decimal PrincipalPart,
    decimal InterestPart,
    DateTime DueDate,
    bool IsPaid,
    DateTime? PaidAt);