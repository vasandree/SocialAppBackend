using System.ComponentModel.DataAnnotations;
using Common.Models.Dtos;

namespace PersonService.Application.Dtos.Responses;

public class PaginatedPersonsDto
{
    [Required]
    public List<PersonDto> Person { get; set; }
    
    [Required]
    public Pagination Pagination { get; set; }
}