using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace UserModule.Tests.Entities;

public class UserSettingsTests
{
    private static ApplicationUser CreateUser() => new("first", "last", "username", null);

    [Fact]
    public void Ctor_WithUserLanguageChatInstance_SetsAllFieldsCorrectly()
    {
        // Arrange
        var user = CreateUser();
        var language = Language.Ru;
        long chatInstance = 123456789;
        var timeZone = "Europe/Moscow";

        // Act
        var settings = new UserSettings(user, language, chatInstance, timeZone);

        // Assert
        Assert.Equal(user.Id, settings.UserId);
        Assert.Equal(user, settings.User);

        Assert.Equal(language, settings.Language);
        Assert.Equal(chatInstance.ToString(), settings.ChatInstance);
        Assert.Equal(Theme.Light, settings.Theme);
        Assert.True(settings.TaskNotifications);
        Assert.True(settings.EventNotifications);
        Assert.Equal(timeZone, settings.TimeZoneId);
    }

    [Fact]
    public void Ctor_WithUserOnly_SetsDefaultValues()
    {
        // Arrange
        var user = CreateUser();

        // Act
        var settings = new UserSettings(user);

        // Assert
        Assert.Equal(user.Id, settings.UserId);
        Assert.Equal(user, settings.User);

        Assert.Equal(Language.En, settings.Language);
        Assert.Null(settings.ChatInstance);
        Assert.Equal(Theme.Light, settings.Theme);
        Assert.True(settings.TaskNotifications);
        Assert.True(settings.EventNotifications);
        Assert.Equal("UTC", settings.TimeZoneId);
    }

    [Fact]
    public void UpdateSettings_ChangesAllMutableSettings()
    {
        // Arrange
        var user = CreateUser();
        var settings = new UserSettings(user);

        var newLanguage = Language.Ru;
        var newTheme = Theme.Dark;
        var newTaskNotifications = false;
        var newEventNotifications = true;
        var newTimeZoneId = "Europe/Berlin";

        var originalUserId = settings.UserId;
        var originalUser = settings.User;

        // Act
        settings.UpdateSettings(
            newLanguage,
            newTheme,
            newTaskNotifications,
            newEventNotifications,
            newTimeZoneId);

        // Assert 
        Assert.Equal(newLanguage, settings.Language);
        Assert.Equal(newTheme, settings.Theme);
        Assert.Equal(newTaskNotifications, settings.TaskNotifications);
        Assert.Equal(newEventNotifications, settings.EventNotifications);
        Assert.Equal(newTimeZoneId, settings.TimeZoneId);

        Assert.Equal(originalUserId, settings.UserId);
        Assert.Equal(originalUser, settings.User);
    }
}