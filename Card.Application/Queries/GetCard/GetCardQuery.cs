using Card.Application.Abstractions;
using Card.Application.DTOs;

namespace Card.Application.Queries.GetCard;

public record GetCardQuery(Guid CardId) : IQuery<CardDto?>;