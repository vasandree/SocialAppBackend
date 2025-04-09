using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PersonService.Application.Dtos.Requests;

public class PersonRequestDto
{
    [Required]
    public string Name { get; set;}
    
    public string? Description { get; set;}
    
    public string? Email { get; set;}
    
    public string? PhoneNumber { get; set;}
    
    public IFormFile? Avatar { get; set; }
}