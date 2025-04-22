using System.Security.Claims;

namespace User.Contracts.Helpers;

public interface IJwtService
{
    string GenerateTokenString(string username, Guid id);
    ClaimsPrincipal? GetTokenPrincipal(string token);
    string GenerateRefreshTokenString();

}