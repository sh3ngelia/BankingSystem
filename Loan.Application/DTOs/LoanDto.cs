namespace Loan.Application.DTOs;

public record LoanDto(
    Guid Id,
    Guid ApplicantId,
    decimal Amount,
    decimal InterestRate,
    int TermMonths,
    string Type,
    string Status,
    int? CreditScore,
    string? RejectionReason,
    DateTime CreatedAt,
    DateTime? ApprovedAt,
    RepaymentScheduleDto? RepaymentSchedule);