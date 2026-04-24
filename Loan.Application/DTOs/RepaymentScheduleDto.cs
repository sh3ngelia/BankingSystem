namespace Loan.Application.DTOs;

public record RepaymentScheduleDto(
    Guid Id,
    decimal TotalAmount,
    decimal TotalInterest,
    int RemainingInstallments,
    decimal RemainingAmount,
    IEnumerable<InstallmentDto> Installments);