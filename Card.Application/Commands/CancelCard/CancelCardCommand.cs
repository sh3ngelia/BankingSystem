using Card.Application.Abstractions;

namespace Card.Application.Commands.CancelCard;

public record CancelCardCommand(Guid CardId) : ICommand;