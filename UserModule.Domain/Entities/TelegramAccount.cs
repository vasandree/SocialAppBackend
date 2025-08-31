using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserModule.Domain.Enums;

namespace UserModule.Domain.Entities;

public class TelegramAccount
{
    private TelegramAccount() { }
    
    public TelegramAccount(long id, string username, string? firstName, string? lastName, string? languageCode,
        bool allowsWriteToPm, string? photoUrl, ApplicationUser user)
    {
        Id = id;
        Userneme = username;
        FirstName = firstName;
        LastName = lastName;
        LanguageCode = languageCode;
        AllowsWriteToPm = allowsWriteToPm;
        PhotoUrl = photoUrl;
        User = user;
        UserId = user.Id;
    }

    [Key] public long Id { get; private set; }

    [Required] public string Userneme { get; private set; }

    private string? FirstName { get; set; }
    private string? LastName { get; set; }
    private string? LanguageCode { get; set; }
    private bool AllowsWriteToPm { get; set; }
    private string? PhotoUrl { get; set; }

    [Required] public ApplicationUser User { get; private init; }

    [Required] [ForeignKey("User")] public Guid UserId { get; private init; }

    public void UpdateInfo(string username, string? firstName, string? lastName, string? photoUrl, string? languageCode,
        bool allowsWriteToPm)
    {
        Userneme = username;
        FirstName = firstName;
        LastName = lastName;
        PhotoUrl = photoUrl;
        LanguageCode = languageCode;
        AllowsWriteToPm = allowsWriteToPm;
    }
}