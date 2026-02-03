using BankingSystem.Application.DTOs.Auth;

namespace BankingSystem.Application.Interfaces;
public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
}