using BankingSystem.Shared.Results;
using Transaction.Application.Abstractions;
using Transaction.Domain.Events;

namespace Transaction.Application.Commands.CompleteTransaction;

public class CompleteTransactionCommandHandler : ICommandHandler<CompleteTransactionCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CompleteTransactionCommand command, CancellationToken ct)
    {
        var transaction = await _transactionRepository.GetByIdAsync(command.TransactionId, ct);
        if (transaction is null)
            return Result.Failure("Transaction not found.");

        transaction.Complete();

        await _transactionRepository.UpdateAsync(transaction, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new TransactionCompletedEvent(
            transaction.Id,
            transaction.FromAccountId,
            transaction.ToAccountId,
            transaction.Amount,
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}