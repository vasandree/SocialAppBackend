using System.Security.Claims;

namespace UserService.Application.Helpers.JwtService;

public interface IJwtService
{
    string GenerateTokenString(string username, Guid id);
    ClaimsPrincipal? GetTokenPrincipal(string token);
    string GenerateRefreshTokenString();

}