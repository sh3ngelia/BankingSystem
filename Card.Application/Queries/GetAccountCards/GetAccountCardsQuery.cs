using Card.Application.Abstractions;
using Card.Application.DTOs;

namespace Card.Application.Queries.GetAccountCards;

public record GetAccountCardsQuery(Guid AccountId) : IQuery<IEnumerable<CardDto>>;