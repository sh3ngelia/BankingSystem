using BankingSystem.Shared.BaseTypes;
using Loan.Domain.Exceptions;

namespace Loan.Domain.Entities;

public class Installment : Entity
{
    public Guid LoanId { get; private set; }
    public int InstallmentNumber { get; private set; }
    public decimal Amount { get; private set; }
    public decimal PrincipalPart { get; private set; }
    public decimal InterestPart { get; private set; }
    public DateTime DueDate { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTime? PaidAt { get; private set; }

    private Installment() { }

    internal static Installment Create(
        Guid loanId,
        int number,
        decimal amount,
        decimal principalPart,
        decimal interestPart,
        DateTime dueDate)
    {
        return new Installment
        {
            LoanId = loanId,
            InstallmentNumber = number,
            Amount = amount,
            PrincipalPart = principalPart,
            InterestPart = interestPart,
            DueDate = dueDate,
            IsPaid = false
        };
    }

    public void Pay()
    {
        if (IsPaid)
            throw new InstallmentAlreadyPaidException(Id);

        IsPaid = true;
        PaidAt = DateTime.UtcNow;
    }
}