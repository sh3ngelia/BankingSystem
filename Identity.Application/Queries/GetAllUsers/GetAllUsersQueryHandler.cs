using Identity.Application.Abstractions;
using Identity.Application.DTOs;

namespace Identity.Application.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery query, CancellationToken ct)
    {
        var users = await _userRepository.GetAllAsync(ct);

        return users.Select(user => new UserDto(
            user.Id,
            user.Email.Value,
            user.FirstName,
            user.LastName,
            user.Status.ToString(),
            user.Roles.Select(r => r.Name),
            user.CreatedAt));
    }
}