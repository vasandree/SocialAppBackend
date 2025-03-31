using System.ComponentModel.DataAnnotations;
using Common;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class User: BaseEntity
{
    
    public string? FirstName {get; set;}
    
    public string? LastName {get; set;}
    
    [Required]
    public string UserName {get; set;}
    
    public string? PhotoUrl {get; set;}
    
    [Required]
    public Language Language {get; set;}
    
    public TelegramAccount TelegramAccount {get; set;}
    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}