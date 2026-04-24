using Card.Application.Abstractions;

namespace Card.Application.Commands.UpdateCardLimits;

public record UpdateCardLimitsCommand(
    Guid CardId,
    decimal DailyLimit,
    decimal MonthlyLimit) : ICommand;