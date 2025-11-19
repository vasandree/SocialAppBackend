using System.Reflection;
using NotificationModule.Domain;

namespace NotificationModule.Tests;

public class OutboxPayloadFactoryTests
{
    private static DateTime InvokeConvertToUserTime(DateTime utcDate, string? timeZoneId)
    {
        var method = typeof(OutboxPayloadFactory)
            .GetMethod("ConvertToUserTime", BindingFlags.NonPublic | BindingFlags.Static)!;

        return (DateTime)method.Invoke(null, [utcDate, timeZoneId])!;
    }

    private static string InvokeFormatDateTime(DateTime dt)
    {
        var method = typeof(OutboxPayloadFactory)
            .GetMethod("FormatDateTime", BindingFlags.NonPublic | BindingFlags.Static)!;

        return (string)method.Invoke(null, new object?[] { dt })!;
    }

    private static string InvokeReplace(string template, IDictionary<string, string?> values)
    {
        var method = typeof(OutboxPayloadFactory)
            .GetMethod("Replace", BindingFlags.NonPublic | BindingFlags.Static)!;

        return (string)method.Invoke(null, new object?[] { template, values })!;
    }


    [Fact]
    public void ConvertToUserTime_WhenTimeZoneIsNull_ReturnsOriginalUtc()
    {
        // Arrange
        var utcDate = new DateTime(2025, 2, 10, 12, 0, 0, DateTimeKind.Utc);

        // Act
        var result = InvokeConvertToUserTime(utcDate, null);

        // Assert
        Assert.Equal(utcDate, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void ConvertToUserTime_WhenTimeZoneIsWhitespace_ReturnsOriginalUtc()
    {
        // Arrange
        var utcDate = new DateTime(2025, 2, 10, 12, 0, 0, DateTimeKind.Utc);

        // Act
        var result = InvokeConvertToUserTime(utcDate, "   ");

        // Assert
        Assert.Equal(utcDate, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void ConvertToUserTime_WhenTimeZoneIsValid_UtcToThatZone()
    {
        // Arrange
        var utcDate = new DateTime(2025, 2, 10, 12, 0, 0, DateTimeKind.Utc);
        var tz = TimeZoneInfo.Utc; // гарантированно есть

        // Act
        var result = InvokeConvertToUserTime(utcDate, tz.Id);

        // Assert
        var expected = TimeZoneInfo.ConvertTimeFromUtc(utcDate, tz);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertToUserTime_WhenTimeZoneIsInvalid_FallsBackToOriginalUtc()
    {
        // Arrange
        var utcDate = new DateTime(2025, 2, 10, 12, 0, 0, DateTimeKind.Utc);
        var invalidTz = "This/TimeZone/DoesNotExist";

        // Act
        var result = InvokeConvertToUserTime(utcDate, invalidTz);

        // Assert
        Assert.Equal(utcDate, result);
    }


    [Fact]
    public void FormatDateTime_FormatsInExpectedPattern()
    {
        // Arrange
        var dt = new DateTime(2025, 2, 3, 9, 5, 0);

        // Act
        var result = InvokeFormatDateTime(dt);

        // Assert
        Assert.Equal("03.02.2025 09:05", result);
    }


    [Fact]
    public void Replace_ReplacesAllPlaceholdersWithValues()
    {
        // Arrange
        var template = "Hello {{Name}}, your task is due at {{Time}}.";
        var values = new Dictionary<string, string?>
        {
            ["Name"] = "Andrei",
            ["Time"] = "10:30"
        };

        // Act
        var result = InvokeReplace(template, values);

        // Assert
        Assert.Equal("Hello Andrei, your task is due at 10:30.", result);
    }

    [Fact]
    public void Replace_UsesEmptyString_WhenValueIsNull()
    {
        // Arrange
        var template = "Hello {{Name}}, description: {{Description}}.";
        var values = new Dictionary<string, string?>
        {
            ["Name"] = "Andrei",
            ["Description"] = null
        };

        // Act
        var result = InvokeReplace(template, values);

        // Assert
        Assert.Equal("Hello Andrei, description: .", result);
    }

    [Fact]
    public void Replace_DoesNotTouchNonExistingPlaceholders()
    {
        // Arrange
        var template = "Hello {{Name}}, today is {{Day}}.";
        var values = new Dictionary<string, string?>
        {
            ["Name"] = "Andrei"
            // "Day" отсутствует
        };

        // Act
        var result = InvokeReplace(template, values);

        // Assert
        Assert.Equal("Hello Andrei, today is {{Day}}.", result);
    }

    [Fact]
    public void Replace_IsCaseSensitiveForKeys()
    {
        // Arrange
        var template = "Hello {{Name}} and {{name}}.";
        var values = new Dictionary<string, string?>
        {
            ["Name"] = "Upper",
            ["name"] = "Lower"
        };

        // Act
        var result = InvokeReplace(template, values);

        // Assert
        Assert.Equal("Hello Upper and Lower.", result);
    }
}