using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace UserModule.Tests.Entities;

public class ApplicationUserTests
{
    [Fact]
    public void Constructor1_SetsBasicFieldsCorrectly()
    {
        // Arrange
        string? first = "John";
        string? last = "Doe";
        string username = "johndoe";
        string? photo = "https://photo.url";

        // Act
        var user = new ApplicationUser(first, last, username, photo);

        // Assert
        Assert.Null(user.Email);
        Assert.Null(user.Password);
        Assert.Null(user.TelegramAccount);
        Assert.Null(user.UserSettings);
    }

    [Fact]
    public void Constructor2_SetsAllProvidedFieldsCorrectly()
    {
        // Arrange
        string email = "user@example.com";
        string username = "user123";
        string first = "Mike";
        string last = "Smith";
        string password = "hashed_pass";

        // Act
        var user = new ApplicationUser(email, first, last, username, password);

        // Assert
        Assert.Equal(username, user.UserName);
        Assert.Null(user.PhotoUrl);
        Assert.Null(user.TelegramAccount);
        Assert.Null(user.UserSettings);
    }

    [Fact]
    public void UpdateInfo_UpdatesUserNameAndNamesAndPhoto()
    {
        // Arrange
        var user = new ApplicationUser("old@mail.com", "Old", "User", "old_username", "hash");

        var newUsername = "new_username";
        var newFirst = "NewFirst";
        var newLast = "NewLast";
        var newPhoto = "https://new.photo";

        // Act
        user.UpdateInfo(newUsername, newFirst, newLast, newPhoto);

        // Assert
        Assert.Equal(newUsername, user.UserName);
    }

    [Fact]
    public void AddTelegramAccount_AssignsTelegramAccount()
    {
        // Arrange
        var appUser = new ApplicationUser("First", "Last", "user", null);

        var telegram = new TelegramAccount(
            123,
            "tg_user",
            "First",
            "Last",
            "en",
            true,
            "url",
            appUser
        );

        // Act
        appUser.AddTelegramAccount(telegram);

        // Assert
        Assert.Equal(telegram, appUser.TelegramAccount);
    }

    [Fact]
    public void AddSettings_AssignsUserSettings()
    {
        // Arrange
        var appUser = new ApplicationUser("First", "Last", "user", null);
        var settings = new UserSettings(appUser, Language.En, 456, "UTC");

        // Act
        appUser.AddSettings(settings);

        // Assert
        Assert.Equal(settings, appUser.UserSettings);
    }

    [Fact]
    public void UserName_IsMutable()
    {
        // Arrange
        var user = new ApplicationUser("First", "Last", "initialName", null);

        // Act
        user.UserName = "updatedName";

        // Assert
        Assert.Equal("updatedName", user.UserName);
    }
}