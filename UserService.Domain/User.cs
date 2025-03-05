using System.ComponentModel.DataAnnotations;

namespace UserService.Domain;

public class User
{
    [Key]
    public Guid Id {get; init;} = Guid.NewGuid();
    
    [Required]
    public long TelegramId {get; set;}
    
    public string? FirstName {get; set;}
    
    public string? LastName {get; set;}
    
    [Required]
    public string UserName {get; set;}
    
    public string? PhotoUrl {get; set;}
    
    [Required]
    public string LanguageCode {get; set;}
}