using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PersonService.Application.Dtos.Requests;

public class PlaceRequestDto
{
    [Required]
    public string Name { get; set;}
    
    public string? Description { get; set;}

    public IFormFile? Avatar { get; set; }
}