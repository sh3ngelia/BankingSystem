using BankingSystem.Application.DTOs.Auth;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Enums;
using BankingSystem.Domain.Interfaces;


namespace BankingSystem.Application.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;


    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task RegisterAsync(RegisterDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = UserRole.User
        };


        await _userRepository.AddAsync(user);
    }


    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email)
                   ?? throw new Exception("Invalid credentials");


        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");


        return "JWT_TOKEN_PLACEHOLDER";
    }
}