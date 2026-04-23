using Identity.Application.Abstractions;
using Identity.Application.DTOs;

namespace Identity.Application.Queries.GetUser;

public record GetUserQuery(Guid UserId) : IQuery<UserDto?>;