using Shared.Domain;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.Tests.Entities;

public class PersonsAccountTests
{
    [Fact]
    public void Constructor_SetsAllPropertiesCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var username = "test_user";
        var type = SocialNetwork.Telegram; 

        // Act
        var account = new PersonsAccount(userId, username, type, personId);

        // Assert
        Assert.Equal(userId, account.CreatorId);
        Assert.Equal(username, account.Username);
        Assert.Equal(type, account.Type);
        Assert.Equal(personId, account.PersonsId);
    }

    [Fact]
    public void UpdateUsername_ChangesUsername()
    {
        // Arrange
        var account = new PersonsAccount(
            Guid.NewGuid(),
            "old_name",
            SocialNetwork.Telegram,
            Guid.NewGuid());

        var newUsername = "new_name";

        // Act
        account.UpdateUsername(newUsername);

        // Assert
        Assert.Equal(newUsername, account.Username);
    }

    [Fact]
    public void IsUserCreator_ReturnsTrue_WhenUserIsCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var account = new PersonsAccount(
            creatorId,
            "user",
            SocialNetwork.Telegram,
            Guid.NewGuid());

        // Act
        var result = account.IsUserCreator(creatorId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsUserCreator_ReturnsFalse_WhenUserIsNotCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();

        var account = new PersonsAccount(
            creatorId,
            "user",
            SocialNetwork.Telegram,
            Guid.NewGuid());

        // Act
        var result = account.IsUserCreator(anotherUserId);

        // Assert
        Assert.False(result);
    }
}