using Identity.Application.Abstractions;
using Identity.Application.DTOs;

namespace Identity.Application.Queries.GetAllUsers;

public record GetAllUsersQuery : IQuery<IEnumerable<UserDto>>;