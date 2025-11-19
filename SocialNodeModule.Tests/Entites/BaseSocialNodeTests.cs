using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.Tests.Entites;

public class BaseSocialNodeTests
{
    // Вспомогательный класс, чтобы протестировать сам BaseSocialNode
    private sealed class TestSocialNode(Guid id, string name, string? description, string? avatarUrl, Guid userId)
        : BaseSocialNode(id, name, description, avatarUrl, userId);

    [Fact]
    public void BaseConstructor_SetsProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Node";
        var description = "Desc";
        var avatarUrl = "https://avatar";

        // Act
        var node = new TestSocialNode(id, name, description, avatarUrl, userId);

        // Assert
        Assert.Equal(id, node.Id);
        Assert.Equal(name, node.Name);
        Assert.Equal(description, node.Description);
        Assert.Equal(avatarUrl, node.AvatarUrl);
        Assert.Equal(userId, node.CreatorId);
    }

    [Fact]
    public void EditInfo_UpdatesFields_ForBaseClass()
    {
        // Arrange
        var node = new TestSocialNode(
            Guid.NewGuid(),
            "Old name",
            "Old desc",
            "https://old",
            Guid.NewGuid());

        var newName = "New name";
        var newDesc = "New desc";
        var newAvatar = "https://new";

        // Act
        node.EditInfo(newName, newDesc, newAvatar);

        // Assert
        Assert.Equal(newName, node.Name);
        Assert.Equal(newDesc, node.Description);
        Assert.Equal(newAvatar, node.AvatarUrl);
    }
}