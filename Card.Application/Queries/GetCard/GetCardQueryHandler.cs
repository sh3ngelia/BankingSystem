using Card.Application.Abstractions;
using Card.Application.DTOs;

namespace Card.Application.Queries.GetCard;

public class GetCardQueryHandler : IQueryHandler<GetCardQuery, CardDto?>
{
    private readonly ICardRepository _cardRepository;

    public GetCardQueryHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<CardDto?> Handle(GetCardQuery query, CancellationToken ct)
    {
        var card = await _cardRepository.GetByIdAsync(query.CardId, ct);
        if (card is null) return null;

        return new CardDto(
            card.Id,
            card.AccountId,
            card.CardNumber.Masked,
            card.ExpiryDate.ToString(),
            card.Type.ToString(),
            card.Status.ToString(),
            card.DailyLimit,
            card.MonthlyLimit,
            card.IssuedAt);
    }
}