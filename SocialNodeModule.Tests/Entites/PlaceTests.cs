using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.Tests.Entites;

public class PlaceTests
{
    [Fact]
    public void Constructor_SetsAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "My place";
        var description = "Some description";
        var avatarUrl = "https://example.com/avatar.png";

        // Act
        var place = new Place(id, name, description, avatarUrl, userId);

        // Assert
        Assert.Equal(id, place.Id);
        Assert.Equal(name, place.Name);
        Assert.Equal(description, place.Description);
        Assert.Equal(avatarUrl, place.AvatarUrl);
        Assert.Equal(userId, place.CreatorId);
    }

    [Fact]
    public void EditInfo_UpdatesNameDescriptionAndAvatar()
    {
        // Arrange
        var place = new Place(
            Guid.NewGuid(),
            "Old name",
            "Old description",
            "https://old.avatar",
            Guid.NewGuid());

        var newName = "New name";
        var newDescription = "New description";
        var newAvatar = "https://new.avatar";

        // Act
        place.EditInfo(newName, newDescription, newAvatar);

        // Assert
        Assert.Equal(newName, place.Name);
        Assert.Equal(newDescription, place.Description);
        Assert.Equal(newAvatar, place.AvatarUrl);
    }

    [Fact]
    public void IsUserCreator_ReturnsTrue_WhenUserIsCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var place = new Place(
            Guid.NewGuid(),
            "Name",
            null,
            null,
            creatorId);

        // Act
        var result = place.IsUserCreator(creatorId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsUserCreator_ReturnsFalse_WhenUserIsNotCreator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var place = new Place(
            Guid.NewGuid(),
            "Name",
            null,
            null,
            creatorId);

        // Act
        var result = place.IsUserCreator(anotherUserId);

        // Assert
        Assert.False(result);
    }
}