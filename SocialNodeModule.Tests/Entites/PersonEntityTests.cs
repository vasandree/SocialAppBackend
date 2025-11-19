using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.Tests.Entites;

public class PersonEntityTests
{
    [Fact]
    public void Constructor_SetsBaseAndOwnFields()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "John Doe";
        var description = "Some desc";
        var avatar = "https://example.com/a.png";
        var email = "john@example.com";
        var phone = "+123456";

        // Act
        var person = new PersonEntity(id, name, description, avatar, userId, email, phone);

        // Assert 
        Assert.Equal(id, person.Id);
        Assert.Equal(name, person.Name);
        Assert.Equal(description, person.Description);
        Assert.Equal(avatar, person.AvatarUrl);
        Assert.Equal(userId, person.CreatorId);
    }

    [Fact]
    public void EditInfo_UpdatesBaseFields_AndEmailAndPhone()
    {
        // Arrange
        var person = new PersonEntity(
            Guid.NewGuid(),
            "Old name",
            "Old desc",
            "https://old.png",
            Guid.NewGuid(),
            "old@mail.com",
            "000");

        var newName = "New name";
        var newDesc = "New desc";
        var newAvatar = "https://new.png";
        var newEmail = "new@mail.com";
        var newPhone = "111";

        // Act
        person.EditInfo(newName, newDesc, newAvatar, newEmail, newPhone);

        // Assert 
        Assert.Equal(newName, person.Name);
        Assert.Equal(newDesc, person.Description);
        Assert.Equal(newAvatar, person.AvatarUrl);
    }

    [Fact]
    public void IsUserCreator_WorksForPersonEntity()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var person = new PersonEntity(
            Guid.NewGuid(),
            "Name",
            null,
            null,
            creatorId,
            null,
            null);

        // Act + Assert
        Assert.True(person.IsUserCreator(creatorId));
        Assert.False(person.IsUserCreator(Guid.NewGuid()));
    }
}