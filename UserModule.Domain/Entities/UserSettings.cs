using System.ComponentModel.DataAnnotations;
using Shared.Domain;
using UserModule.Domain.Enums;

namespace UserModule.Domain.Entities;

public class UserSettings : BaseEntity
{
    private UserSettings() { }
    
    public UserSettings(ApplicationUser user, Language language, string? chatInstance)
    {
        UserId = user.Id;
        User = user;
        Language = language;
        ChatInstance = chatInstance;
        Theme = Theme.Light;
    }
    
    public Guid UserId { get; private init; }
    
    public ApplicationUser User { get; private set; }
    
    [Required]
    private Language Language {get; set;}
    
    [Required]
    private string ChatInstance { get; set; }
    
    [Required]
    private Theme Theme {get; set;}

    public void UpdateSettings(Language language, Theme theme)
    {
        Language = language;
        Theme = theme;
    }
}