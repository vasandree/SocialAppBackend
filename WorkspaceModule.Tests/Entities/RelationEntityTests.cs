using WorkspaceModule.Domain.Entities;

namespace WorkspaceModule.Tests.Entities;

public class RelationEntityTests
{
    [Fact]
    public void Constructor_SetsAllFieldsCorrectly()
    {
        // Arrange
        var name = "Friendship";
        var description = "They know each other from school";
        var color = "#FF0000";

        var firstNode = Guid.NewGuid();
        var secondNode = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var workspace = new WorkspaceEntity("WS", "Desc", userId);

        // Act
        var rel = new RelationEntity(
            name,
            description,
            color,
            firstNode,
            secondNode,
            workspace,
            userId);

        // Assert — публичные/доступные поля
        Assert.Equal(firstNode, rel.FirstSocialNode);
        Assert.Equal(secondNode, rel.SecondSocialNode);
        Assert.Equal(userId, rel.CreatorId);
        Assert.Equal(name, rel.Name);
        Assert.Equal(description, rel.Description);
        Assert.Equal(color, rel.Color);
    }

    [Fact]
    public void UpdateInfo_UpdatesNameDescriptionAndColor()
    {
        // Arrange
        var rel = new RelationEntity(
            "Old name",
            "Old desc",
            "#000000",
            Guid.NewGuid(),
            Guid.NewGuid(),
            new WorkspaceEntity("WS", "Desc", Guid.NewGuid()),
            Guid.NewGuid());

        var newName = "New name";
        var newDesc = "New description";
        var newColor = "#FFFFFF";

        // Act
        rel.UpdateInfo(newName, newDesc, newColor);

        // Assert
        Assert.Equal(newName, rel.Name);
        Assert.Equal(newDesc, rel.Description);
        Assert.Equal(newColor, rel.Color);
    }

    [Fact]
    public void FirstAndSecondSocialNode_AreImmutableFromOutside()
    {
        // Arrange
        var first = Guid.NewGuid();
        var second = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var ws = new WorkspaceEntity("WS", "Desc", userId);

        var rel = new RelationEntity("Name", null, "#123", first, second, ws, userId);

        // Act + Assert
        Assert.Equal(first, rel.FirstSocialNode);
        Assert.Equal(second, rel.SecondSocialNode);
    }

    [Fact]
    public void CreatorId_SetFromConstructorUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var ws = new WorkspaceEntity("WS", "Desc", userId);

        // Act
        var rel = new RelationEntity(
            "Name",
            null,
            "#123",
            Guid.NewGuid(),
            Guid.NewGuid(),
            ws,
            userId);

        // Assert
        Assert.Equal(userId, rel.CreatorId);
    }

    [Fact]
    public void CheckIfUserIsCreator_ReturnsFalse_WhenUserIsNotCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        
        var rel = new RelationEntity(
            "Old name",
            "Old desc",
            "#000000",
            Guid.NewGuid(),
            Guid.NewGuid(),
            new WorkspaceEntity("WS", "Desc", Guid.NewGuid()),
            Guid.NewGuid());

        // Act
        var result = rel.IsUserCreator(creatorId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsUserCreator_UsesSameLogicAsCheckIfUserIsCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var otherUser = Guid.NewGuid();
        
        var rel = new RelationEntity(
            "Old name",
            "Old desc",
            "#000000",
            Guid.NewGuid(),
            Guid.NewGuid(),
            new WorkspaceEntity("WS", "Desc", creatorId),
            creatorId);

        // Act + Assert
        Assert.True(rel.IsUserCreator(creatorId));
        Assert.False(rel.IsUserCreator(otherUser));
    }
}