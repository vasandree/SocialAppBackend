using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace NotificationModule.Domain;

public static class OutboxPayloadFactory
{
    private static readonly Dictionary<string, TemplateEnvelope> TemplateCache = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Lock TemplateLock = new();

    public static string BuildTaskReminderText(
        string taskName,
        DateTime endDateUtc,
        string timeZoneId,
        string language)
    {
        var localEnd = ConvertToUserTime(endDateUtc, timeZoneId);

        var template = LoadTemplate("TaskReminder");
        var loc = template.Localizations.FirstOrDefault(l =>
                      l.Language.Equals(language, StringComparison.OrdinalIgnoreCase))
                  ?? template.Localizations.First();

        var dict = new Dictionary<string, string?>
        {
            ["Name"] = taskName,
            ["LocalEnd"] = FormatDateTime(localEnd)
        };

        return Replace(loc.Body, dict);
    }

    public static string BuildEventReminderText(
        string eventName,
        DateTime startDateUtc,
        DateTime endDateUtc,
        string timeZoneId,
        string language)
    {
        var localStart = ConvertToUserTime(startDateUtc, timeZoneId);
        var localEnd = ConvertToUserTime(endDateUtc, timeZoneId);

        var template = LoadTemplate("EventReminder");
        var loc = template.Localizations.FirstOrDefault(l =>
                      l.Language.Equals(language, StringComparison.OrdinalIgnoreCase))
                  ?? template.Localizations.First();

        var dict = new Dictionary<string, string?>
        {
            ["Name"] = eventName,
            ["LocalStart"] = FormatDateTime(localStart),
            ["LocalEnd"] = FormatDateTime(localEnd)
        };

        return Replace(loc.Body, dict);
    }

    private static DateTime ConvertToUserTime(DateTime utcDate, string timeZoneId)
    {
        if (string.IsNullOrWhiteSpace(timeZoneId))
            return utcDate;

        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDate, tz);
        }
        catch
        {
            return utcDate;
        }
    }

    private static string FormatDateTime(DateTime dt)
        => dt.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

    private static string Replace(string template, IDictionary<string, string?> values)
    {
        var result = template;
        foreach (var kv in values)
            result = result.Replace("{{" + kv.Key + "}}", kv.Value ?? string.Empty, StringComparison.Ordinal);
        return result;
    }

    private static TemplateEnvelope LoadTemplate(string name)
    {
        lock (TemplateLock)
        {
            if (TemplateCache.TryGetValue(name, out var cached))
                return cached;

            var asm = Assembly.GetExecutingAssembly();
            var allResources = asm.GetManifestResourceNames();

            var resourceName = allResources
                .FirstOrDefault(r => r.Contains($"{name}.json", StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
                throw new InvalidOperationException($"Template '{name}' not found as embedded resource.");

            using var stream = asm.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            var template = JsonSerializer.Deserialize<TemplateEnvelope>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (template?.Localizations == null || template.Localizations.Count == 0)
                throw new InvalidOperationException($"Template '{name}' is empty or corrupted. JSON:\n{json}");

            TemplateCache[name] = template;
            return template;
        }
    }

    private sealed record TemplateEnvelope
    {
        public string Type { get; set; } = string.Empty;
        public List<TemplateLocalization> Localizations { get; set; } = new();
    }

    private sealed record TemplateLocalization
    {
        public string Language { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
