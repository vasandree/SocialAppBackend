using Shared.Domain;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.Tests.Entities;

public class SocialNetworkAccountTests
{
    private sealed class TestSocialAccount : SocialNetworkAccount
    {
        public TestSocialAccount(Guid creatorId, string username, SocialNetwork type)
        {
            CreatorId = creatorId;
            UpdateUsername(username);
            Type = type;
        }
    }

    [Fact]
    public void UpdateUsername_ChangesUsername_ForBaseClass()
    {
        // Arrange
        var acc = new TestSocialAccount(Guid.NewGuid(), "old_name", SocialNetwork.Telegram);
        var newName = "new_name";

        // Act
        acc.UpdateUsername(newName);

        // Assert
        Assert.Equal(newName, acc.Username);
    }

    [Fact]
    public void IsUserCreator_WorksOnBaseClass()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var acc = new TestSocialAccount(creatorId, "user", SocialNetwork.Telegram);

        // Act + Assert
        Assert.True(acc.IsUserCreator(creatorId));
        Assert.False(acc.IsUserCreator(Guid.NewGuid()));
    }
}