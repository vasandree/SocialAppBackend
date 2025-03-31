using System.ComponentModel.DataAnnotations;
using Common.Models.Dtos;

namespace PersonService.Application.Dtos.Responses;

public class PaginatedPlacesDto
{
    [Required]
    public List<PlaceDto> Place { get; set; }
    
    [Required]
    public Pagination Pagination { get; set; }
}