using System.ComponentModel.DataAnnotations;
using Shared.Domain;
using User.Domain.Enums;

namespace User.Domain.Entities;

public class UserSettings : BaseEntity
{
    public Guid UserId { get; set; }
    
    public ApplicationUser User { get; set; }
    
    [Required]
    public Language Language {get; set;}
    
    [Required]
    public string ChatInstance { get; set; }
    
    [Required]
    public Theme Theme {get; set;}
}