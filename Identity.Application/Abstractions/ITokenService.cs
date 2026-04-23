using Identity.Domain.Entities;

namespace Identity.Application.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}