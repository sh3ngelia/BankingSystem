namespace Identity.Application.DTOs;

public record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Status,
    IEnumerable<string> Roles,
    DateTime CreatedAt);