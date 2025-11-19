using AuthModule.Domain.Entites;

namespace AuthModule.Tests.Entities;

public class RefreshTokenTests
{
    [Fact]
    public void RefreshTokenConstructor()
    {
        // Arrange 

        var userId = Guid.NewGuid();
        var tokenString = "sample_refresh_token";
        var expiresAt = DateTime.UtcNow.AddDays(7);

        // Act
        var token = new RefreshToken(userId, tokenString, expiresAt);

        // Assert
        Assert.Equal(tokenString, token.Token);
        Assert.Equal(expiresAt, token.Expires);
        Assert.True(token.IsOwnedByUser(userId));
    }

    [Fact]
    public void CheckExpired_ReturnsTrue_WhenTokenIsExpired()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tokenString = "expired_token";
        var expiresAt = DateTime.UtcNow.AddDays(-1);

        // Act
        var token = new RefreshToken(userId, tokenString, expiresAt);
        var isExpired = token.CheckExpired();

        // Assert
        Assert.True(isExpired);
    }

    [Fact]
    public void CheckExpired_ReturnsFalse_WhenTokenIsNotExpired()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tokenString = "valid_token";
        var expiresAt = DateTime.UtcNow.AddDays(1);

        // Act
        var token = new RefreshToken(userId, tokenString, expiresAt);
        var isExpired = token.CheckExpired();

        // Assert
        Assert.False(isExpired);
    }

    [Fact]
    public void IsOwnedByUser_ReturnsFalse_WhenUserIdDoesNotMatch()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var differentUserId = Guid.NewGuid();
        var tokenString = "some_token";
        var expiresAt = DateTime.UtcNow.AddDays(1);

        // Act
        var token = new RefreshToken(userId, tokenString, expiresAt);
        var isOwned = token.IsOwnedByUser(differentUserId);

        // Assert
        Assert.False(isOwned);
    }

    [Fact]
    public void IsOwnedByUser_ReturnsTrue_WhenUserIdMatches()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tokenString = "some_token";
        var expiresAt = DateTime.UtcNow.AddDays(1);

        // Act
        var token = new RefreshToken(userId, tokenString, expiresAt);
        var isOwned = token.IsOwnedByUser(userId);

        // Assert
        Assert.True(isOwned);
    }
}