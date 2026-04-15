namespace Loan.Domain.Exceptions;

public class LoanNotFoundException : Exception
{
    public LoanNotFoundException(Guid loanId)
        : base($"Loan with id '{loanId}' was not found.") { }
}