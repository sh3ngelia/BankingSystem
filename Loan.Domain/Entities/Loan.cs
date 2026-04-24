using BankingSystem.Shared.BaseTypes;
using Loan.Domain.Enums;
using Loan.Domain.Events;
using Loan.Domain.Exceptions;
using Loan.Domain.ValueObjects;

namespace Loan.Domain.Entities;

public class Loan : AggregateRoot
{
    public Guid ApplicantId { get; private set; }
    public decimal Amount { get; private set; }
    public InterestRate InterestRate { get; private set; }
    public int TermMonths { get; private set; }
    public LoanType Type { get; private set; }
    public LoanStatus Status { get; private set; }
    public CreditScore? CreditScore { get; private set; }
    public string? RejectionReason { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ApprovedAt { get; private set; }

    private RepaymentSchedule? _repaymentSchedule;
    public RepaymentSchedule? RepaymentSchedule => _repaymentSchedule;

    private Loan() { }

    public static Loan Apply(
        Guid applicantId,
        decimal amount,
        decimal interestRate,
        int termMonths,
        LoanType type)
    {
        if (amount <= 0)
            throw new ArgumentException("Loan amount must be greater than zero.");

        if (termMonths <= 0)
            throw new ArgumentException("Term must be greater than zero.");

        var loan = new Loan
        {
            ApplicantId = applicantId,
            Amount = amount,
            InterestRate = InterestRate.Create(interestRate),
            TermMonths = termMonths,
            Type = type,
            Status = LoanStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        loan.AddDomainEvent(new LoanAppliedEvent(
            loan.Id,
            applicantId,
            amount,
            DateTime.UtcNow));

        return loan;
    }

    public void Approve(CreditScore creditScore)
    {
        if (Status != LoanStatus.Pending)
            throw new InvalidOperationException("Only pending loans can be approved.");

        CreditScore = creditScore;
        Status = LoanStatus.Approved;
        ApprovedAt = DateTime.UtcNow;

        _repaymentSchedule = GenerateSchedule();

        AddDomainEvent(new LoanApprovedEvent(
            Id,
            ApplicantId,
            Amount,
            DateTime.UtcNow));
    }

    public void Reject(string reason)
    {
        if (Status != LoanStatus.Pending)
            throw new InvalidOperationException("Only pending loans can be rejected.");

        Status = LoanStatus.Rejected;
        RejectionReason = reason;

        AddDomainEvent(new LoanRejectedEvent(
            Id,
            ApplicantId,
            reason,
            DateTime.UtcNow));
    }

    public void Activate()
    {
        if (Status != LoanStatus.Approved)
            throw new LoanNotApprovedException(Id);

        Status = LoanStatus.Active;
    }

    public void PayInstallment()
    {
        if (Status != LoanStatus.Active)
            throw new InvalidOperationException("Loan is not active.");

        var installment = _repaymentSchedule?.GetNextUnpaid()
            ?? throw new InvalidOperationException("No unpaid installments found.");

        installment.Pay();

        if (_repaymentSchedule.RemainingInstallments == 0)
        {
            Status = LoanStatus.Closed;
            AddDomainEvent(new LoanClosedEvent(Id, DateTime.UtcNow));
        }
    }

    private RepaymentSchedule GenerateSchedule()
    {
        var monthlyRate = InterestRate.AsDecimalFraction() / 12;
        var installments = new List<Installment>();

        decimal monthlyPayment;

        if (monthlyRate == 0)
        {
            monthlyPayment = Amount / TermMonths;
        }
        else
        {
            var pow = (decimal)Math.Pow((double)(1 + monthlyRate), TermMonths);
            monthlyPayment = Amount * monthlyRate * pow / (pow - 1);
        }

        var remainingPrincipal = Amount;

        for (int i = 1; i <= TermMonths; i++)
        {
            var interestPart = remainingPrincipal * monthlyRate;
            var principalPart = monthlyPayment - interestPart;
            remainingPrincipal -= principalPart;

            var installment = Installment.Create(
                Id,
                i,
                Math.Round(monthlyPayment, 2),
                Math.Round(principalPart, 2),
                Math.Round(interestPart, 2),
                DateTime.UtcNow.AddMonths(i));

            installments.Add(installment);
        }

        var schedule = RepaymentSchedule.Create(Id, installments);
        schedule.AddInstallments(installments);
        return schedule;
    }
}