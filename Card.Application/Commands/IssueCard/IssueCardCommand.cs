using Card.Application.Abstractions;
using Card.Domain.Enums;

namespace Card.Application.Commands.IssueCard;

public record IssueCardCommand(
    Guid AccountId,
    CardType Type,
    decimal DailyLimit,
    decimal MonthlyLimit) : ICommand<Guid>;