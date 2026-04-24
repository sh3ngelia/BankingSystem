using BankingSystem.Shared.Results;
using Loan.Application.Abstractions;
using LoanEntity = Loan.Domain.Entities.Loan;
using Loan.Domain.Events;

namespace Loan.Application.Commands.ApplyForLoan;

public class ApplyForLoanCommandHandler : ICommandHandler<ApplyForLoanCommand, Guid>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public ApplyForLoanCommandHandler(
        ILoanRepository loanRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(ApplyForLoanCommand command, CancellationToken ct)
    {
        var loan = LoanEntity.Apply(
            command.ApplicantId,
            command.Amount,
            command.InterestRate,
            command.TermMonths,
            command.Type);

        await _loanRepository.AddAsync(loan, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new LoanAppliedEvent(
            loan.Id,
            loan.ApplicantId,
            loan.Amount,
            DateTime.UtcNow), ct);

        return Result<Guid>.Success(loan.Id);
    }
}