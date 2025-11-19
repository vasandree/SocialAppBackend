
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthModule.UseCases.Implementation.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthModule.Tests.Services
{
    public class JwtServiceTests
    {
        private const string ValidKey = "super_secret_test_key_1234567890";
        private const string AnotherKey = "another_super_secret_key_0987654321";

        private IConfiguration BuildConfiguration(string key)
        {
            var dict = new Dictionary<string, string?>
            {
                ["Jwt:Key"] = key
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(dict)
                .Build();
        }

        private JwtService CreateService(string key = ValidKey)
        {
            var config = BuildConfiguration(key);
            return new JwtService(config);
        }

        [Fact]
        public void GenerateTokenString_ShouldReturn_NotEmptyToken()
        {
            // Arrange
            var service = CreateService();
            var userId = Guid.NewGuid();
            var username = "test_user";

            // Act
            var token = service.GenerateTokenString(username, userId);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(token));
        }

        [Fact]
        public void GenerateTokenString_ShouldContainExpectedClaims()
        {
            // Arrange
            var service = CreateService();
            var userId = Guid.NewGuid();
            var username = "test_user";

            // Act
            var token = service.GenerateTokenString(username, userId);

            // Разбираем JWT без валидации подписи
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert

            var userIdClaim = jwt.Claims.SingleOrDefault(c => c.Type == "UserId");
            var usernameClaim = jwt.Claims.SingleOrDefault(c => c.Type == "Username");
            var nameIdClaim = jwt.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            Assert.NotNull(userIdClaim);
            Assert.NotNull(usernameClaim);
            Assert.NotNull(nameIdClaim);

            Assert.Equal(userId.ToString(), userIdClaim!.Value);
            Assert.Equal(username, usernameClaim!.Value);
            Assert.Equal(userId.ToString(), nameIdClaim!.Value);
        }

        [Fact]
        public void GenerateTokenString_ShouldHaveLifetimeAbout60Minutes()
        {
            // Arrange
            var service = CreateService();
            var userId = Guid.NewGuid();
            var username = "test_user";

            var before = DateTime.UtcNow;

            // Act
            var token = service.GenerateTokenString(username, userId);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert
            Assert.InRange(
                jwt.ValidTo,
                before.AddMinutes(59),
                before.AddMinutes(61)
            );
        }

        [Fact]
        public void GetTokenPrincipal_WithValidToken_ShouldReturnPrincipalWithClaims()
        {
            // Arrange
            var service = CreateService();
            var userId = Guid.NewGuid();
            var username = "test_user";

            var token = service.GenerateTokenString(username, userId);

            // Act
            var principal = service.GetTokenPrincipal(token);

            // Assert
            Assert.NotNull(principal);
            var identity = principal!.Identity as ClaimsIdentity;
            Assert.NotNull(identity);
            Assert.True(identity!.IsAuthenticated);

            var userIdClaim = identity.FindFirst("UserId");
            var usernameClaim = identity.FindFirst("Username");
            var nameIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            Assert.NotNull(userIdClaim);
            Assert.NotNull(usernameClaim);
            Assert.NotNull(nameIdClaim);

            Assert.Equal(userId.ToString(), userIdClaim!.Value);
            Assert.Equal(username, usernameClaim!.Value);
            Assert.Equal(userId.ToString(), nameIdClaim!.Value);
        }

        [Fact]
        public void GetTokenPrincipal_WithTokenSignedWithAnotherKey_ShouldThrow()
        {
            // Arrange
            var serviceWithValidKey = CreateService();
            var serviceWithAnotherKey = CreateService(AnotherKey);

            var userId = Guid.NewGuid();
            var username = "test_user";

            var token = serviceWithValidKey.GenerateTokenString(username, userId);

            // Act + Assert
            Assert.Throws<SecurityTokenSignatureKeyNotFoundException>(() =>
            {
                serviceWithAnotherKey.GetTokenPrincipal(token);
            });
        }

        [Fact]
        public void GetTokenPrincipal_WithInvalidTokenString_ShouldThrow()
        {
            // Arrange
            var service = CreateService();
            var invalidToken = "this_is_not_a_valid_jwt";

            // Act + Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                service.GetTokenPrincipal(invalidToken);
            });
        }
    }
}
