using BankingSystem.Shared.Results;
using Loan.Application.Abstractions;
using Loan.Domain.Events;
using Loan.Domain.ValueObjects;

namespace Loan.Application.Commands.ApproveLoan;

public class ApproveLoanCommandHandler : ICommandHandler<ApproveLoanCommand>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveLoanCommandHandler(
        ILoanRepository loanRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ApproveLoanCommand command, CancellationToken ct)
    {
        var loan = await _loanRepository.GetByIdAsync(command.LoanId, ct);
        if (loan is null)
            return Result.Failure("Loan not found.");

        var creditScore = CreditScore.Create(command.CreditScore);

        if (creditScore.IsPoor)
            return Result.Failure("Credit score is too low for approval.");

        loan.Approve(creditScore);

        await _loanRepository.UpdateAsync(loan, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new LoanApprovedEvent(
            loan.Id,
            loan.ApplicantId,
            loan.Amount,
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}