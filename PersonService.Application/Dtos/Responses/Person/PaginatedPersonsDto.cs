using System.ComponentModel.DataAnnotations;
using Common.Models.Dtos;

namespace PersonService.Application.Dtos.Responses.Person;

public class PaginatedPersonsDto
{
    [Required]
    public List<ListedBaseSocialNodeDto> Person { get; set; }
    
    [Required]
    public Pagination Pagination { get; set; }
}