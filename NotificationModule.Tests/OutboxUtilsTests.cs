

using NotificationModule.Domain;

namespace NotificationModule.Tests;

public class OutboxUtilsTests
{
    [Fact]
    public void GetScheduledUtcForReminder_WhenTimeZoneIsNull_UsesUtc()
    {
        // Arrange
        var eventDate = new DateTime(2025, 2, 10, 15, 0, 0, DateTimeKind.Unspecified);

        // Act
        var result = OutboxUtils.GetScheduledUtcForReminder(eventDate, null);

        // Assert
        var expectedUtc = DateTime.SpecifyKind(eventDate, DateTimeKind.Utc).AddDays(-1);
        Assert.Equal(expectedUtc, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void GetScheduledUtcForReminder_WhenTimeZoneIsWhitespace_UsesUtc()
    {
        // Arrange
        var eventDate = new DateTime(2025, 2, 10, 15, 0, 0, DateTimeKind.Unspecified);

        // Act
        var result = OutboxUtils.GetScheduledUtcForReminder(eventDate, "   ");

        // Assert
        var expectedUtc = DateTime.SpecifyKind(eventDate, DateTimeKind.Utc).AddDays(-1);
        Assert.Equal(expectedUtc, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void GetScheduledUtcForReminder_WhenTimeZoneIsInvalid_FallsBackToUtc()
    {
        // Arrange
        var eventDate = new DateTime(2025, 2, 10, 15, 0, 0, DateTimeKind.Unspecified);
        var invalidTimeZoneId = "This/TimeZone/DoesNotExist";

        // Act
        var result = OutboxUtils.GetScheduledUtcForReminder(eventDate, invalidTimeZoneId);

        // Assert
        var expectedUtc = DateTime.SpecifyKind(eventDate, DateTimeKind.Utc).AddDays(-1);
        Assert.Equal(expectedUtc, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void GetScheduledUtcForReminder_WhenEventDateIsUtc_IgnoresTimeZoneAndSubtractsOneDay()
    {
        // Arrange
        var tzId = TimeZoneInfo.Local.Id; // любой валидный ID
        var eventDateUtc = new DateTime(2025, 2, 10, 10, 0, 0, DateTimeKind.Utc);

        // Act
        var result = OutboxUtils.GetScheduledUtcForReminder(eventDateUtc, tzId);

        // Assert
        var expected = eventDateUtc.AddDays(-1);
        Assert.Equal(expected, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void GetScheduledUtcForReminder_WhenKindIsUnspecified_UsesProvidedTimeZone()
    {
        // Arrange
        var tz = TimeZoneInfo.Local; // гарантированно существует
        var eventDate = new DateTime(2025, 2, 10, 12, 0, 0, DateTimeKind.Unspecified);

        // Act
        var result = OutboxUtils.GetScheduledUtcForReminder(eventDate, tz.Id);

        // Assert
        var expectedUtc = TimeZoneInfo.ConvertTimeToUtc(eventDate, tz).AddDays(-1);
        Assert.Equal(expectedUtc, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void GetScheduledUtcForReminder_WhenKindIsLocal_TreatedAsUnspecifiedForGivenTimeZone()
    {
        // Arrange
        var tz = TimeZoneInfo.Local;
        var eventDateLocalKind = new DateTime(2025, 2, 10, 12, 0, 0, DateTimeKind.Local);

        // Act
        var result = OutboxUtils.GetScheduledUtcForReminder(eventDateLocalKind, tz.Id);

        // Assert
        // Логика метода: переприсвоить Kind в Unspecified и конвертировать относительно tz
        var asUnspecified = DateTime.SpecifyKind(eventDateLocalKind, DateTimeKind.Unspecified);
        var expectedUtc = TimeZoneInfo.ConvertTimeToUtc(asUnspecified, tz).AddDays(-1);

        Assert.Equal(expectedUtc, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void GetScheduledUtcForReminder_AlwaysSubtractsOneDayFromConvertedUtc()
    {
        // Arrange
        var tz = TimeZoneInfo.Local;
        var eventDate = new DateTime(2025, 2, 20, 8, 0, 0, DateTimeKind.Unspecified);

        // Act
        var result = OutboxUtils.GetScheduledUtcForReminder(eventDate, tz.Id);

        // Assert
        var convertedUtc = TimeZoneInfo.ConvertTimeToUtc(eventDate, tz);
        var expected = convertedUtc.AddDays(-1);

        Assert.Equal(expected, result);
    }
}