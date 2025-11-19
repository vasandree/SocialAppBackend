using System.Text.Json;
using System.Text.Json.Nodes;
using WorkspaceModule.Domain.Entities;

namespace WorkspaceModule.Tests.Entities;

public class WorkspaceEntityTests
{
    [Fact]
    public void Constructor_SetsNameDescriptionAndCreator()
    {
        // Arrange
        var name = "My workspace";
        var description = "Some description";
        var creatorId = Guid.NewGuid();

        // Act
        var ws = new WorkspaceEntity(name, description, creatorId);

        // Assert
        Assert.Equal(creatorId, ws.CreatorId);
        Assert.Equal("{}", ws.ContentJson);
        Assert.NotNull(ws.SocialNodes);
        Assert.NotNull(ws.Relations);
        Assert.NotNull(ws.Tasks);
        Assert.NotNull(ws.Events);
        Assert.Equal(name, ws.Name);
        Assert.Equal(description, ws.Description);
    }

    [Fact]
    public void UpdateInfo_UpdatesNameAndDescription()
    {
        // Arrange
        var ws = new WorkspaceEntity("Old name", "Old desc", Guid.NewGuid());

        var newName = "New name";
        var newDesc = "New description";

        // Act
        ws.UpdateInfo(newName, newDesc);

        // Assert
        Assert.Equal(newName, ws.Name);
        Assert.Equal(newDesc, ws.Description);
    }

    [Fact]
    public void CheckIfUserIsCreator_ReturnsTrue_WhenUserIsCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var ws = new WorkspaceEntity("Name", "Desc", creatorId);

        // Act
        var result = ws.CheckIfUserIsCreator(creatorId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckIfUserIsCreator_ReturnsFalse_WhenUserIsNotCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var otherUser = Guid.NewGuid();
        var ws = new WorkspaceEntity("Name", "Desc", creatorId);

        // Act
        var result = ws.CheckIfUserIsCreator(otherUser);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsUserCreator_UsesSameLogicAsCheckIfUserIsCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var otherUser = Guid.NewGuid();
        var ws = new WorkspaceEntity("Name", "Desc", creatorId);

        // Act + Assert
        Assert.True(ws.IsUserCreator(creatorId));
        Assert.False(ws.IsUserCreator(otherUser));
    }

    [Fact]
    public void RemoveEvent_RemovesEventIdFromEventsCollection()
    {
        // Arrange
        var ws = new WorkspaceEntity("Name", "Desc", Guid.NewGuid());
        var eventId = Guid.NewGuid();
        ws.Events.Add(eventId);

        // Act
        ws.RemoveEvent(eventId);

        // Assert
        Assert.DoesNotContain(eventId, ws.Events);
    }

    [Fact]
    public void RemoveEvent_DoesNothing_WhenEventIdNotInCollection()
    {
        // Arrange
        var ws = new WorkspaceEntity("Name", "Desc", Guid.NewGuid());
        ws.Events.Add(Guid.NewGuid());
        var initialCount = ws.Events.Count;
        var notExistingId = Guid.NewGuid();

        // Act
        ws.RemoveEvent(notExistingId);

        // Assert
        Assert.Equal(initialCount, ws.Events.Count);
    }

    [Fact]
    public void UpdateContent_ChangesContentJson()
    {
        // Arrange
        var ws = new WorkspaceEntity("Name", "Desc", Guid.NewGuid());
        var newJson = "{\"test\":123}";

        // Act
        ws.UpdateContent(newJson);

        // Assert
        Assert.Equal(newJson, ws.ContentJson);
    }

    [Fact]
    public void Content_Get_DeserializesContentJson()
    {
        // Arrange
        var ws = new WorkspaceEntity("Name", "Desc", Guid.NewGuid());
        ws.ContentJson = "{\"foo\":\"bar\",\"num\":42}";

        // Act
        JsonObject content = ws.Content;

        // Assert
        Assert.Equal("bar", content["foo"]!.GetValue<string>());
        Assert.Equal(42, content["num"]!.GetValue<int>());
    }

    [Fact]
    public void Content_Init_SetsContentJson()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var contentObject = new JsonObject
        {
            ["field"] = "value",
            ["n"] = 10
        };

        // Act
        var ws = new WorkspaceEntity("Name", "Desc", creatorId)
        {
            Content = contentObject
        };

        // Assert
        var expectedJson = JsonSerializer.Serialize(contentObject);
        Assert.Equal(expectedJson, ws.ContentJson);
    }
}