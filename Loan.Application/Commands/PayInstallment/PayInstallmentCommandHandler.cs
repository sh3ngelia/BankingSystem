using BankingSystem.Shared.Results;
using Loan.Application.Abstractions;
using Loan.Domain.Events;

namespace Loan.Application.Commands.PayInstallment;

public class PayInstallmentCommandHandler : ICommandHandler<PayInstallmentCommand>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public PayInstallmentCommandHandler(
        ILoanRepository loanRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(PayInstallmentCommand command, CancellationToken ct)
    {
        var loan = await _loanRepository.GetByIdAsync(command.LoanId, ct);
        if (loan is null)
            return Result.Failure("Loan not found.");

        loan.PayInstallment();

        await _loanRepository.UpdateAsync(loan, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        if (loan.Status == Loan.Domain.Enums.LoanStatus.Closed)
        {
            await _eventBus.PublishAsync(new LoanClosedEvent(
                loan.Id,
                DateTime.UtcNow), ct);
        }

        return Result.Success();
    }
}