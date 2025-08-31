using Shared.Contracts.Dtos;

namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;

public record PaginatedClusterDto(List<ListedBaseSocialNodeDto> Cluster, Pagination Pagination);