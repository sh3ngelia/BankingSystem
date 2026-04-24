using Card.Application.Abstractions;

namespace Card.Application.Commands.UnfreezeCard;

public record UnfreezeCardCommand(Guid CardId) : ICommand;