using System.ComponentModel.DataAnnotations;
using User.Domain.Enums;

namespace User.Contracts.Dtos.Responses;

public class UserDto
{
    [Required]
    public Guid Id {get; set;} 
    
    [Required]
    public long TelegramId {get; set;}
    
    public string? FirstName {get; set;}
    
    public string? LastName {get; set;}
    
    [Required]
    public string UserName {get; set;}
    
    public string? PhotoUrl {get; set;}
    
    [Required]
    public Language LanguageCode {get; set;}
}