using Card.Application.Abstractions;

namespace Card.Application.Commands.FreezeCard;

public record FreezeCardCommand(Guid CardId) : ICommand;