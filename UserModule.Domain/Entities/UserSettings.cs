using System.ComponentModel.DataAnnotations;
using Shared.Domain;
using UserModule.Domain.Enums;

namespace UserModule.Domain.Entities;

public class UserSettings : BaseEntity
{
    private UserSettings()
    {
    }

    public UserSettings(ApplicationUser user, Language language, string? chatInstance)
    {
        UserId = user.Id;
        User = user;
        Language = language;
        ChatInstance = chatInstance;
        Theme = Theme.Light;
        TaskNotifications = true;
        EventNotifications = true;
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
    }

    public Guid UserId { get; private init; }

    public ApplicationUser User { get; private set; }

    [Required] public Language Language { get; private set; }

    [Required] private string? ChatInstance { get; set; }

    [Required] public Theme Theme { get; private set; }

    public bool TaskNotifications { get; private set; }
    public bool EventNotifications { get; private set; }

    public void UpdateSettings(Language language, Theme theme, bool taskNotifications, bool eventNotifications)
    {
        Language = language;
        Theme = theme;
        TaskNotifications = taskNotifications;
        EventNotifications = eventNotifications;
    }
}