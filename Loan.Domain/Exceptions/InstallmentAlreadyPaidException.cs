namespace Loan.Domain.Exceptions;

public class InstallmentAlreadyPaidException : Exception
{
    public InstallmentAlreadyPaidException(Guid installmentId)
        : base($"Installment '{installmentId}' has already been paid.") { }
}