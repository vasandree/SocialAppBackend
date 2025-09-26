using System.ComponentModel.DataAnnotations;
using Shared.Domain;
using UserModule.Domain.Enums;

namespace UserModule.Domain.Entities;

public class UserSettings : BaseEntity
{
    private UserSettings()
    {
    }

    public UserSettings(ApplicationUser user, Language language, long chatInstance, string timeZoneId = "UTC")
    {
        UserId = user.Id;
        User = user;
        Language = language;
        ChatInstance = chatInstance.ToString();
        Theme = Theme.Light;
        TaskNotifications = true;
        EventNotifications = true;
        TimeZoneId = timeZoneId;
    }

    public UserSettings(ApplicationUser user)
    {
        UserId = user.Id;
        User = user;
        Language = Language.En;
        ChatInstance = null;
        Theme = Theme.Light;
        TaskNotifications = true;
        EventNotifications = true;
        TimeZoneId = "UTC";
    }

    public Guid UserId { get; private init; }

    public ApplicationUser User { get; private set; }

    [Required] public Language Language { get; private set; }

    [Required] public string? ChatInstance { get; private set; }

    [Required] public Theme Theme { get; private set; }

    public bool TaskNotifications { get; private set; }
    public bool EventNotifications { get; private set; }

    [Required] public string TimeZoneId { get; private set; } = "UTC";

    public void UpdateSettings(Language language, Theme theme, bool taskNotifications, bool eventNotifications, string timeZoneId)
    {
        Language = language;
        Theme = theme;
        TaskNotifications = taskNotifications;
        EventNotifications = eventNotifications;
        TimeZoneId = timeZoneId;
    }
}