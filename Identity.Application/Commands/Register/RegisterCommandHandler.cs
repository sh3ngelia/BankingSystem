using BankingSystem.Shared.Results;
using Identity.Application.Abstractions;
using Identity.Domain.Entities;

namespace Identity.Application.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterCommand command, CancellationToken ct)
    {
        var exists = await _userRepository.ExistsAsync(command.Email, ct);
        if (exists)
            return Result<Guid>.Failure("User with this email already exists.");

        var passwordHash = _passwordHasher.Hash(command.Password);

        var user = User.Create(
            command.Email,
            passwordHash,
            command.FirstName,
            command.LastName);

        var role = await _roleRepository.GetByNameAsync("Customer", ct);
        if (role is not null)
            user.AssignRole(role);

        await _userRepository.AddAsync(user, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result<Guid>.Success(user.Id);
    }
}