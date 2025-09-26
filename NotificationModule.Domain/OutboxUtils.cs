namespace NotificationModule.Domain;

public static class OutboxUtils
{
    public static DateTime GetScheduledUtcForReminder(DateTime eventDateLocal, string? timeZoneId)
    {
        TimeZoneInfo tz;
        if (string.IsNullOrWhiteSpace(timeZoneId))
        {
            tz = TimeZoneInfo.Utc;
        }
        else
        {
            try
            {
                tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                tz = TimeZoneInfo.Utc;
            }
            catch (InvalidTimeZoneException)
            {
                tz = TimeZoneInfo.Utc;
            }
        }

        DateTime eventDateUtc;
        if (eventDateLocal.Kind == DateTimeKind.Utc)
        {
            eventDateUtc = eventDateLocal;
        }
        else
        {
            if (eventDateLocal.Kind != DateTimeKind.Unspecified)
                eventDateLocal = DateTime.SpecifyKind(eventDateLocal, DateTimeKind.Unspecified);
            eventDateUtc = TimeZoneInfo.ConvertTimeToUtc(eventDateLocal, tz);
        }

        return eventDateUtc.AddDays(-1);
    }
}
