using TaskModule.Domain.Entites;
using TaskModule.Domain.Enums;

namespace TaskModule.Tests.Entities;

public class TaskEntityTests
{
    [Fact]
    public void Constructor_SetsAllPropertiesCorrectly()
    {
        // Arrange
        var name = "Test task";
        var description = "Some description";
        var startDate = new DateTime(2025, 2, 1, 10, 0, 0, DateTimeKind.Utc);
        var endDate = new DateTime(2025, 2, 5, 18, 0, 0, DateTimeKind.Utc);
        var socialNodeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var workspaceId = Guid.NewGuid();

        // Act
        var task = new TaskEntity(name, description, startDate, endDate, socialNodeId, userId, workspaceId);

        // Assert
        Assert.Equal(name, task.Name);
        Assert.Equal(description, task.Description);
        Assert.Equal(startDate, task.CreateDate);
        Assert.Equal(endDate, task.EndDate);
        Assert.Equal(socialNodeId, task.SocialNodeId);
        Assert.Equal(userId, task.CreatorId);
        Assert.Equal(workspaceId, task.WorkspaceId);

        // Статус по умолчанию
        Assert.Equal(StatusOfTask.Created, task.Status);
    }

    [Fact]
    public void ChangeStatusTo_ChangesStatus()
    {
        // Arrange
        var task = new TaskEntity(
            "Task",
            null,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        var newStatus = StatusOfTask.InProgress;

        // Act
        task.ChangeStatusTo(newStatus);

        // Assert
        Assert.Equal(newStatus, task.Status);
    }

    [Fact]
    public void UpdateInfo_UpdatesNameDescriptionEndDateAndSocialNode()
    {
        // Arrange
        var initialStart = new DateTime(2025, 2, 1, 10, 0, 0, DateTimeKind.Utc);
        var initialEnd = new DateTime(2025, 2, 3, 10, 0, 0, DateTimeKind.Utc);
        var initialSocialNodeId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();
        var workspaceId = Guid.NewGuid();

        var task = new TaskEntity(
            "Old name",
            "Old desc",
            initialStart,
            initialEnd,
            initialSocialNodeId,
            creatorId,
            workspaceId);

        var newName = "New name";
        var newDesc = "New desc";
        var newEnd = new DateTime(2025, 2, 10, 18, 0, 0, DateTimeKind.Utc);
        var newSocialNodeId = Guid.NewGuid();

        // Act
        task.UpdateInfo(newName, newDesc, newEnd, newSocialNodeId);

        // Assert — обновились нужные поля
        Assert.Equal(newName, task.Name);
        Assert.Equal(newDesc, task.Description);
        Assert.Equal(newEnd, task.EndDate);
        Assert.Equal(newSocialNodeId, task.SocialNodeId);

        // Assert — не тронули то, что не должно меняться
        Assert.Equal(initialStart, task.CreateDate);
        Assert.Equal(creatorId, task.CreatorId);
        Assert.Equal(workspaceId, task.WorkspaceId);
    }

    [Fact]
    public void IsUserCreator_ReturnsTrue_WhenUserIsCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var task = new TaskEntity(
            "Task",
            null,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            Guid.NewGuid(),
            creatorId,
            Guid.NewGuid());

        // Act
        var result = task.IsUserCreator(creatorId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsUserCreator_ReturnsFalse_WhenUserIsNotCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        var task = new TaskEntity(
            "Task",
            null,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            Guid.NewGuid(),
            creatorId,
            Guid.NewGuid());

        // Act
        var result = task.IsUserCreator(otherUserId);

        // Assert
        Assert.False(result);
    }
}