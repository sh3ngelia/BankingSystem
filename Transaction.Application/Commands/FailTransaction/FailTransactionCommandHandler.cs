using BankingSystem.Shared.Results;
using Transaction.Application.Abstractions;
using Transaction.Domain.Events;

namespace Transaction.Application.Commands.FailTransaction;

public class FailTransactionCommandHandler : ICommandHandler<FailTransactionCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public FailTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(FailTransactionCommand command, CancellationToken ct)
    {
        var transaction = await _transactionRepository.GetByIdAsync(command.TransactionId, ct);
        if (transaction is null)
            return Result.Failure("Transaction not found.");

        transaction.Fail(command.Reason);

        await _transactionRepository.UpdateAsync(transaction, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new TransactionFailedEvent(
            transaction.Id,
            command.Reason,
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}