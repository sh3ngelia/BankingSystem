using Identity.Application.Abstractions;
using System.Text.Json.Serialization;

namespace Identity.Application.Commands.Register;

public record RegisterCommand(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("firstName")] string FirstName,
    [property: JsonPropertyName("lastName")] string LastName) : ICommand<Guid>;