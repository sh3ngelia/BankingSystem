using BankingSystem.Shared.Results;
using Card.Application.Abstractions;
using CardEntity = Card.Domain.Entities.Card;
using Card.Domain.Events;

namespace Card.Application.Commands.IssueCard;

public class IssueCardCommandHandler : ICommandHandler<IssueCardCommand, Guid>
{
    private readonly ICardRepository _cardRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public IssueCardCommandHandler(
        ICardRepository cardRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _cardRepository = cardRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(IssueCardCommand command, CancellationToken ct)
    {
        var card = CardEntity.Issue(
            command.AccountId,
            command.Type,
            command.DailyLimit,
            command.MonthlyLimit);

        await _cardRepository.AddAsync(card, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new CardIssuedEvent(
            card.Id,
            card.AccountId,
            card.Type.ToString(),
            DateTime.UtcNow), ct);

        return Result<Guid>.Success(card.Id);
    }
}