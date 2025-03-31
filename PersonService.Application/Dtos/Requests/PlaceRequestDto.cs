using System.ComponentModel.DataAnnotations;

namespace PersonService.Application.Dtos.Requests;

public class PlaceRequestDto
{
    [Required]
    public string Name { get; set;}
    
    public string? Description { get; set;}

    public string? avatarUrl { get; set; }
}