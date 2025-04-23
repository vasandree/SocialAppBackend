using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Dtos;

namespace SocialNode.Contracts.Dtos.Responses.ClusterOfPeople;

public class PaginatedClusterDto
{
    [Required] 
    public List<ListedBaseSocialNodeDto> Cluster { get; set; }
    
    [Required]
    public Pagination Pagination { get; set; }
}