using BankingSystem.Shared.Results;
using Transaction.Application.Abstractions;
using TransactionEntity = Transaction.Domain.Entities.Transaction;
using Transaction.Domain.Events;

namespace Transaction.Application.Commands.InitiateTransaction;

public class InitiateTransactionCommandHandler : ICommandHandler<InitiateTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionLimitRepository _limitRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public InitiateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        ITransactionLimitRepository limitRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _limitRepository = limitRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(InitiateTransactionCommand command, CancellationToken ct)
    {
        var limit = await _limitRepository.GetByAccountIdAsync(command.FromAccountId, ct);
        if (limit is not null)
            limit.ValidateAndRegister(command.Amount);

        var transaction = TransactionEntity.Initiate(
            command.FromAccountId,
            command.ToAccountId,
            command.Amount,
            command.Currency,
            command.Type);

        await _transactionRepository.AddAsync(transaction, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new TransactionInitiatedEvent(
            transaction.Id,
            transaction.FromAccountId,
            transaction.ToAccountId,
            transaction.Amount,
            transaction.Currency,
            DateTime.UtcNow), ct);

        return Result<Guid>.Success(transaction.Id);
    }
}