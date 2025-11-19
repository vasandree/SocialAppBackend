using EventModule.Domain.Entities;

namespace EventModule.Tests.Entities;

public class EventEntityTests
{
    [Fact]
    public void Constructor_SetsBasicProperties()
    {
        // Arrange
        var socialNodes = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var workspaceId = Guid.NewGuid();
        var date = new DateTime(2025, 1, 1);
        var description = "Test description";
        var location = "Test location";
        var title = "Test title";

        // Act
        var entity = new EventEntity(socialNodes, workspaceId, date, description, location, title);

        // Assert
        Assert.Equal(title, entity.Title);
        Assert.Equal(description, entity.Description);
        Assert.Equal(location, entity.Location);
        Assert.Equal(workspaceId, entity.WorkspaceId);
        Assert.Equal(date, entity.Date);
            
        Assert.Equal(socialNodes, entity.SocialNodeIds);
    }

    [Fact]
    public void AddEventType_SetsEventTypeAndEventTypeId()
    {
        // Arrange
        var eventEntity = new EventEntity(
            new List<Guid>(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            null,
            null,
            "Title");

        var eventType = new EventTypeEntity("Type name", Guid.NewGuid());

        // Act
        eventEntity.AddEventType(eventType);

        // Assert
        Assert.Equal(eventType, eventEntity.EventType);
        Assert.Equal(eventType.Id, eventEntity.EventTypeId);
    }

    [Fact]
    public void RemoveEventType_ClearsEventTypeAndEventTypeId()
    {
        // Arrange
        var eventEntity = new EventEntity(
            new List<Guid>(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            null,
            null,
            "Title");

        var eventType = new EventTypeEntity("Type name", Guid.NewGuid());
        eventEntity.AddEventType(eventType);

        // Act
        eventEntity.RemoveEventType();

        // Assert
        Assert.Null(eventEntity.EventType);
        Assert.Null(eventEntity.EventTypeId);
    }

    [Fact]
    public void CheckIfEventType_ReturnsTrue_WhenIdsMatch()
    {
        // Arrange
        var eventEntity = new EventEntity(
            new List<Guid>(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            null,
            null,
            "Title");

        var eventType = new EventTypeEntity("Type name", Guid.NewGuid());
        eventEntity.AddEventType(eventType);

        // Act
        var result = eventEntity.CheckIfEventType(eventType.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckIfEventType_ReturnsFalse_WhenIdsDoNotMatch()
    {
        // Arrange
        var eventEntity = new EventEntity(
            new List<Guid>(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            null,
            null,
            "Title");

        var eventType = new EventTypeEntity("Type name", Guid.NewGuid());
        eventEntity.AddEventType(eventType);

        // Act
        var result = eventEntity.CheckIfEventType(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CheckIfEventType_ReturnsFalse_WhenEventTypeNotSet()
    {
        // Arrange
        var eventEntity = new EventEntity(
            new List<Guid>(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            null,
            null,
            "Title");

        // Act
        var result = eventEntity.CheckIfEventType(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void UpdateInfo_UpdatesAllFields()
    {
        // Arrange
        var eventEntity = new EventEntity(
            new List<Guid>(),
            Guid.NewGuid(),
            DateTime.UtcNow,
            "Old description",
            "Old location",
            "Old title");

        var newDate = new DateTime(2025, 5, 1);
        var newDescription = "New description";
        var newLocation = "New location";
        var newTitle = "New title";

        // Act
        eventEntity.UpdateInfo(newDate, newDescription, newLocation, newTitle);

        // Assert
        Assert.Equal(newDate, eventEntity.Date);
        Assert.Equal(newDescription, eventEntity.Description);
        Assert.Equal(newLocation, eventEntity.Location);
        Assert.Equal(newTitle, eventEntity.Title);
    }

    [Fact]
    public void AddSocialNode_AddsIdToCollection()
    {
        // Arrange
        var socialNodes = new List<Guid>();
        var eventEntity = new EventEntity(
            socialNodes,
            Guid.NewGuid(),
            DateTime.UtcNow,
            null,
            null,
            "Title");

        var nodeId = Guid.NewGuid();

        // Act
        eventEntity.AddSocialNode(nodeId);

        Assert.Contains(nodeId, eventEntity.SocialNodeIds);
    }

    [Fact]
    public void RemoveSocialNode_RemovesIdFromCollection()
    {
        // Arrange
        var nodeId = Guid.NewGuid();
        var socialNodes = new List<Guid> { nodeId };
        var eventEntity = new EventEntity(
            socialNodes,
            Guid.NewGuid(),
            DateTime.UtcNow,
            null,
            null,
            "Title");

        // Act
        eventEntity.RemoveSocialNode(nodeId);

        // Assert
        Assert.DoesNotContain(nodeId, eventEntity.SocialNodeIds);
    }
}