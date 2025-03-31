using System.ComponentModel.DataAnnotations;
using Common.Models.Dtos;

namespace PersonService.Application.Dtos.Responses;

public class PaginatedClusterDto
{
    [Required] 
    public List<ClusterOfPeopleDto> Cluster { get; set; }
    
    [Required]
    public Pagination Pagination { get; set; }
}