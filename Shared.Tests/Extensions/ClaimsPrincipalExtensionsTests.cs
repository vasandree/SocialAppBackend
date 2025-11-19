using System.Security.Claims;
using Shared.Extensions.Extensions;

namespace Shared.Tests.Extensions;

public class ClaimsPrincipalExtensionsTests
{
    [Fact]
    public void GetUserId_ReturnsGuid_WhenUserIsAuthenticatedAndClaimExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var claims = new[]
        {
            new Claim("UserId", userId.ToString())
        };

        var identity = new ClaimsIdentity(claims, authenticationType: "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        // Act
        var result = principal.GetUserId();

        // Assert
        Assert.Equal(userId, result);
    }

    [Fact]
    public void GetUserId_ThrowsUnauthorized_WhenPrincipalIsNull()
    {
        // Arrange
        ClaimsPrincipal? principal = null;

        // Act + Assert
        var ex = Assert.Throws<UnauthorizedAccessException>(() => principal.GetUserId());
        Assert.Equal("User is not authenticated.", ex.Message);
    }

    [Fact]
    public void GetUserId_ThrowsUnauthorized_WhenIdentityIsNotAuthenticated()
    {
        // Arrange
        var identity = new ClaimsIdentity(); // IsAuthenticated == false
        var principal = new ClaimsPrincipal(identity);

        // Act + Assert
        var ex = Assert.Throws<UnauthorizedAccessException>(() => principal.GetUserId());
        Assert.Equal("User is not authenticated.", ex.Message);
    }

    [Fact]
    public void GetUserId_ThrowsUnauthorized_WhenUserIdClaimMissing()
    {
        // Arrange
        var identity = new ClaimsIdentity([
            new Claim("OtherClaim", "value")
        ], "TestAuth");

        var principal = new ClaimsPrincipal(identity);

        // Act + Assert
        var ex = Assert.Throws<UnauthorizedAccessException>(() => principal.GetUserId());
        Assert.Equal("UserId claim is missing or invalid.", ex.Message);
    }

    [Fact]
    public void GetUserId_ThrowsUnauthorized_WhenUserIdClaimIsInvalidGuid()
    {
        // Arrange
        var identity = new ClaimsIdentity([
            new Claim("UserId", "not-a-guid")
        ], "TestAuth");

        var principal = new ClaimsPrincipal(identity);

        // Act + Assert
        var ex = Assert.Throws<UnauthorizedAccessException>(() => principal.GetUserId());
        Assert.Equal("UserId claim is missing or invalid.", ex.Message);
    }
}