using System.ComponentModel.DataAnnotations;
using Shared.Domain;
using User.Domain.Enums;

namespace User.Domain.Entities;

public class ApplicationUser : BaseEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required] public required string UserName { get; set; }

    public string? PhotoUrl { get; set; }

    public TelegramAccount TelegramAccount { get; set; } = null!;

    public UserSettings UserSettings { get; set; }
}