using BankingSystem.Shared.Results;
using Card.Application.Abstractions;
using Card.Domain.Events;

namespace Card.Application.Commands.FreezeCard;

public class FreezeCardCommandHandler : ICommandHandler<FreezeCardCommand>
{
    private readonly ICardRepository _cardRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public FreezeCardCommandHandler(
        ICardRepository cardRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _cardRepository = cardRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(FreezeCardCommand command, CancellationToken ct)
    {
        var card = await _cardRepository.GetByIdAsync(command.CardId, ct);
        if (card is null)
            return Result.Failure("Card not found.");

        card.Freeze();

        await _cardRepository.UpdateAsync(card, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new CardFrozenEvent(
            card.Id,
            card.AccountId,
            DateTime.UtcNow), ct);

        return Result.Success();
    }
}