using UserModule.Domain.Entities;

namespace UserModule.Tests.Entities;

public class TelegramAccountTests
{
    private readonly ApplicationUser _user = new("firstName", "lastName", "username", "photoUrl");
    
    [Fact]
    public void Constructor_SetsAllFieldsCorrectly()
    {
        // Arrange
        long telegramId = 123456;
        string username = "tg_user";
        string firstName = "John";
        string lastName = "Doe";
        string languageCode = "en";
        bool allowsWrite = true;
        string photoUrl = "https://photo.example.com";


        // Act
        var account = new TelegramAccount(
            telegramId,
            username,
            firstName,
            lastName,
            languageCode,
            allowsWrite,
            photoUrl,
            _user);

        // Assert 
        Assert.Equal(telegramId, account.Id);
        Assert.Equal(username, account.Userneme);
        Assert.Equal(_user, account.User);
        Assert.Equal(_user.Id, account.UserId);
    }

    [Fact]
    public void UpdateInfo_UpdatesAllFields()
    {
        // Arrange

        var account = new TelegramAccount(
            100,
            "old_username",
            "Old",
            "User",
            "ru",
            false,
            "https://old.photo",
            _user);

        var newUsername = "new_username";
        var newFirstName = "NewFirst";
        var newLastName = "NewLast";
        var newPhotoUrl = "https://new.photo";
        var newLanguage = "en";
        var newAllowsWrite = true;

        // Act
        account.UpdateInfo(newUsername, newFirstName, newLastName, newPhotoUrl, newLanguage, newAllowsWrite);

        // Assert 
        Assert.Equal(newUsername, account.Userneme);
    }

    [Fact]
    public void User_IsImmutable()
    {
        // Arrange

        var account = new TelegramAccount(
            1,
            "username",
            null,
            null,
            null,
            false,
            null,
            _user);

        // Assert
        Assert.Equal(_user, account.User);
        Assert.Equal(_user.Id, account.UserId);
    }
}