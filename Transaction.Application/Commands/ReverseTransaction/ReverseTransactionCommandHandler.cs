using BankingSystem.Shared.Results;
using Transaction.Application.Abstractions;
using Transaction.Domain.Events;

namespace Transaction.Application.Commands.ReverseTransaction;

public class ReverseTransactionCommandHandler : ICommandHandler<ReverseTransactionCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public ReverseTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ReverseTransactionCommand command, CancellationToken ct)
    {
        var transaction = await _transactionRepository.GetByIdAsync(command.TransactionId, ct);
        if (transaction is null)
            return Result.Failure("Transaction not found.");

        transaction.Reverse(command.Reason);

        await _transactionRepository.UpdateAsync(transaction, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new TransactionReversedEvent(
            transaction.Id,
            command.Reason,
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}