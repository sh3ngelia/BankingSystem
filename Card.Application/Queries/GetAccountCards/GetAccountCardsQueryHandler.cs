using Card.Application.Abstractions;
using Card.Application.DTOs;

namespace Card.Application.Queries.GetAccountCards;

public class GetAccountCardsQueryHandler : IQueryHandler<GetAccountCardsQuery, IEnumerable<CardDto>>
{
    private readonly ICardRepository _cardRepository;

    public GetAccountCardsQueryHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<IEnumerable<CardDto>> Handle(GetAccountCardsQuery query, CancellationToken ct)
    {
        var cards = await _cardRepository.GetByAccountIdAsync(query.AccountId, ct);

        return cards.Select(card => new CardDto(
            card.Id,
            card.AccountId,
            card.CardNumber.Masked,
            card.ExpiryDate.ToString(),
            card.Type.ToString(),
            card.Status.ToString(),
            card.DailyLimit,
            card.MonthlyLimit,
            card.IssuedAt));
    }
}