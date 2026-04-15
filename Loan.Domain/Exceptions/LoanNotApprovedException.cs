namespace Loan.Domain.Exceptions;

public class LoanNotApprovedException : Exception
{
    public LoanNotApprovedException(Guid loanId)
        : base($"Loan '{loanId}' has not been approved yet.") { }
}