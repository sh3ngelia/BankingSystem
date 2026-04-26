using Identity.Application.Abstractions;
using Identity.Application.DTOs;
using System.Text.Json.Serialization;

namespace Identity.Application.Commands.Login;

public record LoginCommand(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password) : ICommand<AuthResponseDto>;