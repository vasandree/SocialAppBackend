using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Dtos;

namespace SocialNode.Contracts.Dtos.Responses.Person;

public class PaginatedPersonsDto
{
    [Required]
    public List<ListedBaseSocialNodeDto> Person { get; set; }
    
    [Required]
    public Pagination Pagination { get; set; }
}