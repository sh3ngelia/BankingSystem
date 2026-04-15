using BankingSystem.Shared.BaseTypes;

namespace Loan.Domain.Entities;

public class RepaymentSchedule : Entity
{
    public Guid LoanId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal TotalInterest { get; private set; }

    private readonly List<Installment> _installments = new();
    public IReadOnlyList<Installment> Installments => _installments.AsReadOnly();

    public int RemainingInstallments => _installments.Count(i => !i.IsPaid);
    public decimal RemainingAmount => _installments
        .Where(i => !i.IsPaid)
        .Sum(i => i.Amount);

    private RepaymentSchedule() { }

    internal static RepaymentSchedule Create(Guid loanId, List<Installment> installments)
    {
        return new RepaymentSchedule
        {
            LoanId = loanId,
            TotalAmount = installments.Sum(i => i.Amount),
            TotalInterest = installments.Sum(i => i.InterestPart),
            _installments = { }
        };
    }

    internal void AddInstallments(List<Installment> installments)
    {
        _installments.AddRange(installments);
    }

    public Installment? GetNextUnpaid() =>
        _installments
            .Where(i => !i.IsPaid)
            .OrderBy(i => i.DueDate)
            .FirstOrDefault();
}