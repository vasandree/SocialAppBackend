using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace UserModule.Domain.Entities;

public class ApplicationUser : BaseEntity
{
    
    private ApplicationUser() { }
    
    public ApplicationUser(string? firstName, string? lastName, string username, string? photoUrl)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = username;
        PhotoUrl = photoUrl;
    }
    
    public ApplicationUser(string email, string? firstName, string? lastName, string username, string? passwordHash )
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        UserName = username;
        Password = passwordHash;
    }
    
    public string? Email { get; private set; }
    
    public string? Password { get; private set; }

    public string? FirstName { get; private set; }

    public string? LastName { get; private set; }

    [Required] public string UserName { get; set; }

    public string? PhotoUrl { get; private set; }

    public TelegramAccount? TelegramAccount { get; private set; } 

    public UserSettings UserSettings { get; private set; }

    public void AddTelegramAccount(TelegramAccount telegramAccount) => TelegramAccount = telegramAccount;
    public void AddSettings(UserSettings userSettings) => UserSettings = userSettings;

    public void UpdateInfo(string username, string? firstName, string? lastName, string? photoUrl)
    {
        UserName = username;
        FirstName = firstName;
        LastName = lastName;
        PhotoUrl = photoUrl;
    }
}