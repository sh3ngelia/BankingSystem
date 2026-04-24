using BankingSystem.Shared.Results;
using Card.Application.Abstractions;

namespace Card.Application.Commands.UpdateCardLimits;

public class UpdateCardLimitsCommandHandler : ICommandHandler<UpdateCardLimitsCommand>
{
    private readonly ICardRepository _cardRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCardLimitsCommandHandler(
        ICardRepository cardRepository,
        IUnitOfWork unitOfWork)
    {
        _cardRepository = cardRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCardLimitsCommand command, CancellationToken ct)
    {
        var card = await _cardRepository.GetByIdAsync(command.CardId, ct);
        if (card is null)
            return Result.Failure("Card not found.");

        card.UpdateLimits(command.DailyLimit, command.MonthlyLimit);

        await _cardRepository.UpdateAsync(card, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}