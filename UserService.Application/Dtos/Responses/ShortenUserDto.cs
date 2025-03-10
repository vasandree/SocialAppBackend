using System.ComponentModel.DataAnnotations;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Responses;

public class ShortenUserDto
{
    [Required]
    public Guid Id {get; set;} 
    
    public string? FirstName {get; set;}
    
    public string? LastName {get; set;}
    
    [Required]
    public string UserName {get; set;}
    
    public string? PhotoUrl {get; set;}
    
}