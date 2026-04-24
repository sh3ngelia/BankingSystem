using BankingSystem.Shared.Results;
using Loan.Application.Abstractions;
using Loan.Domain.Events;

namespace Loan.Application.Commands.RejectLoan;

public class RejectLoanCommandHandler : ICommandHandler<RejectLoanCommand>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public RejectLoanCommandHandler(
        ILoanRepository loanRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RejectLoanCommand command, CancellationToken ct)
    {
        var loan = await _loanRepository.GetByIdAsync(command.LoanId, ct);
        if (loan is null)
            return Result.Failure("Loan not found.");

        loan.Reject(command.Reason);

        await _loanRepository.UpdateAsync(loan, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new LoanRejectedEvent(
            loan.Id,
            loan.ApplicantId,
            command.Reason,
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}