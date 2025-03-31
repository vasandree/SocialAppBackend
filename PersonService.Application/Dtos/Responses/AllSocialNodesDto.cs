using System.ComponentModel.DataAnnotations;

namespace PersonService.Application.Dtos.Responses;

public class AllSocialNodesDto
{
    [Required]
    public PaginatedPersonsDto Persons { get; set; }
    
    [Required]
    public PaginatedPlacesDto Places { get; set; }
    
    [Required]
    public PaginatedClusterDto Clusters { get; set; }
}