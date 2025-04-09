using System.ComponentModel.DataAnnotations;
using Common.Models.Dtos;

namespace PersonService.Application.Dtos.Responses.Place;

public class PaginatedPlacesDto
{
    [Required]
    public List<ListedBaseSocialNodeDto> Place { get; set; }
    
    [Required]
    public Pagination Pagination { get; set; }
}