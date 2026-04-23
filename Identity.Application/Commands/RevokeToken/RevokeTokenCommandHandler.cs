using BankingSystem.Shared.Results;
using Identity.Application.Abstractions;

namespace Identity.Application.Commands.RevokeToken;

public class RevokeTokenCommandHandler : ICommandHandler<RevokeTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RevokeTokenCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RevokeTokenCommand command, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId, ct);
        if (user is null)
            return Result.Failure("User not found.");

        user.RevokeRefreshToken(command.RefreshToken);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}