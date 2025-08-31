using System.Security.Claims;

namespace UserModule.UseCases.Interfaces.Helpers;

public interface IJwtService
{
    string GenerateTokenString(string username, Guid id);
    ClaimsPrincipal? GetTokenPrincipal(string token);
    string GenerateRefreshTokenString();

}