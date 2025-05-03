using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User.Domain.Enums;

namespace User.Domain.Entities;

public class TelegramAccount
{
    [Key] 
    public long Id { get; set; }

    [Required] 
    public string Userneme { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? LanguageCode { get; set; }
    public bool AllowsWriteToPm { get; set; }
    public string? PhotoUrl { get; set; }

    [Required]
    public ApplicationUser User { get; set; }

    [Required] 
    [ForeignKey("User")] 
    public Guid UserId { get; set; }
}