using BankingSystem.Shared.Results;
using Card.Application.Abstractions;
using Card.Domain.Events;

namespace Card.Application.Commands.CancelCard;

public class CancelCardCommandHandler : ICommandHandler<CancelCardCommand>
{
    private readonly ICardRepository _cardRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public CancelCardCommandHandler(
        ICardRepository cardRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _cardRepository = cardRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CancelCardCommand command, CancellationToken ct)
    {
        var card = await _cardRepository.GetByIdAsync(command.CardId, ct);
        if (card is null)
            return Result.Failure("Card not found.");

        card.Cancel();

        await _cardRepository.UpdateAsync(card, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new CardCancelledEvent(
            card.Id,
            card.AccountId,
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}