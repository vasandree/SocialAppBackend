using EventModule.Domain.Entities;

namespace EventModule.Tests.Entities;

public class EventTypeTests
{
    [Fact]
    public void Constructor_SetsNameAndCreatorId()
    {
        // Arrange
        var name = "Meeting";
        var creatorId = Guid.NewGuid();

        // Act
        var entity = new EventTypeEntity(name, creatorId);

        // Assert
        Assert.Equal(name, entity.Name);
        Assert.Equal(creatorId, entity.CreatorId);
    }

    [Fact]
    public void UpdateName_ChangesName_AndDoesNotChangeCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var initialName = "Old name";
        var newName = "New name";

        var entity = new EventTypeEntity(initialName, creatorId);

        // Act
        entity.UpdateName(newName);

        // Assert
        Assert.Equal(newName, entity.Name);
        Assert.Equal(creatorId, entity.CreatorId);
    }

    [Fact]
    public void IsUserCreator_ReturnsTrue_WhenUserIsCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var entity = new EventTypeEntity("Some event", creatorId);

        // Act
        var result = entity.IsUserCreator(creatorId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsUserCreator_ReturnsFalse_WhenUserIsNotCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var entity = new EventTypeEntity("Some event", creatorId);

        // Act
        var result = entity.IsUserCreator(anotherUserId);

        // Assert
        Assert.False(result);
    }
}