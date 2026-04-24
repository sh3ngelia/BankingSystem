using BankingSystem.Shared.Results;
using Card.Application.Abstractions;
using Card.Domain.Events;

namespace Card.Application.Commands.ProcessCardTransaction;

public class ProcessCardTransactionCommandHandler : ICommandHandler<ProcessCardTransactionCommand, Guid>
{
    private readonly ICardRepository _cardRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public ProcessCardTransactionCommandHandler(
        ICardRepository cardRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _cardRepository = cardRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(ProcessCardTransactionCommand command, CancellationToken ct)
    {
        var card = await _cardRepository.GetByIdAsync(command.CardId, ct);
        if (card is null)
            return Result<Guid>.Failure("Card not found.");

        card.ProcessTransaction(
            command.Amount,
            command.Currency,
            command.MerchantName,
            command.MerchantCategory);

        await _cardRepository.UpdateAsync(card, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        var transaction = card.Transactions.Last();

        await _eventBus.PublishAsync(new CardTransactionProcessedEvent(
            card.Id,
            transaction.Id,
            command.Amount,
            command.MerchantName,
            DateTime.UtcNow), ct);

        return Result<Guid>.Success(transaction.Id);
    }
}