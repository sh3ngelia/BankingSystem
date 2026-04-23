using Identity.Application.Abstractions;
using Identity.Application.DTOs;

namespace Identity.Application.Queries.GetUser;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserQuery query, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, ct);
        if (user is null) return null;

        return new UserDto(
            user.Id,
            user.Email.Value,
            user.FirstName,
            user.LastName,
            user.Status.ToString(),
            user.Roles.Select(r => r.Name),
            user.CreatedAt);
    }
}