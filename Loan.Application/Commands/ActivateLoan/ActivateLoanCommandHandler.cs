using BankingSystem.Shared.Results;
using Loan.Application.Abstractions;

namespace Loan.Application.Commands.ActivateLoan;

public class ActivateLoanCommandHandler : ICommandHandler<ActivateLoanCommand>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateLoanCommandHandler(
        ILoanRepository loanRepository,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActivateLoanCommand command, CancellationToken ct)
    {
        var loan = await _loanRepository.GetByIdAsync(command.LoanId, ct);
        if (loan is null)
            return Result.Failure("Loan not found.");

        loan.Activate();

        await _loanRepository.UpdateAsync(loan, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}