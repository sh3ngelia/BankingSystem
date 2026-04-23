using Identity.Application.Abstractions;
using Identity.Application.DTOs;

namespace Identity.Application.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) : ICommand<AuthResponseDto>;