using BankingSystem.Shared.Results;
using Identity.Application.Abstractions;
using Identity.Application.DTOs;

namespace Identity.Application.Commands.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthResponseDto>> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email, ct);
        if (user is null)
            return Result<AuthResponseDto>.Failure("Invalid email or password.");

        var isValid = _passwordHasher.Verify(command.Password, user.PasswordHash.Value);
        if (!isValid)
            return Result<AuthResponseDto>.Failure("Invalid email or password.");

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = user.GenerateRefreshToken();

        user.RecordLogin();

        await _unitOfWork.SaveChangesAsync(ct);

        var response = new AuthResponseDto(
            accessToken,
            refreshToken.Token,
            refreshToken.ExpiresAt,
            new UserDto(
                user.Id,
                user.Email.Value,
                user.FirstName,
                user.LastName,
                user.Status.ToString(),
                user.Roles.Select(r => r.Name),
                user.CreatedAt));

        return Result<AuthResponseDto>.Success(response);
    }
}