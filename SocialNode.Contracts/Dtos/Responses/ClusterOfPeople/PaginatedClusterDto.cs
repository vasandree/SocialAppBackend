using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Dtos;

namespace SocialNode.Contracts.Dtos.Responses.ClusterOfPeople;

public class PaginatedClusterDto
{
    [Required] 
    public required List<ListedBaseSocialNodeDto> Cluster { get; init; }
    
    [Required]
    public required Pagination Pagination { get; init; }
}