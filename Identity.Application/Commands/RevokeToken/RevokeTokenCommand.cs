using Identity.Application.Abstractions;

namespace Identity.Application.Commands.RevokeToken;

public record RevokeTokenCommand(
    Guid UserId,
    string RefreshToken) : ICommand;