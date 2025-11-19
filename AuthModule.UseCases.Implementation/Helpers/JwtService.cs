using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserModule.UseCases.Interfaces.Helpers;

namespace AuthModule.UseCases.Implementation.Helpers;

internal sealed class JwtService(IConfiguration config) : IJwtService
{
    public string GenerateTokenString(string username, Guid id)
    {
        var claims = new List<Claim>
        {
            new("UserId", id.ToString()),
            new("Username", username),
            new(ClaimTypes.NameIdentifier, id.ToString()),
        };

        var staticKey = config.GetSection("Jwt:Key").Value;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(staticKey));
        var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signingCred
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }

    public ClaimsPrincipal? GetTokenPrincipal(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value));

        var validation = new TokenValidationParameters
        {
            IssuerSigningKey = securityKey,
            ValidateLifetime = false,
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }

    public string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[64];

        using (var numberGenerator = RandomNumberGenerator.Create())
        {
            numberGenerator.GetBytes(randomNumber);
        }

        return Convert.ToBase64String(randomNumber);
    }
}