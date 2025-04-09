using System.ComponentModel.DataAnnotations;
using Common.Models.Dtos;

namespace PersonService.Application.Dtos.Responses.ClusterOfPeople;

public class PaginatedClusterDto
{
    [Required] 
    public List<ListedBaseSocialNodeDto> Cluster { get; set; }
    
    [Required]
    public Pagination Pagination { get; set; }
}