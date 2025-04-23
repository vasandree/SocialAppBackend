using MediatR;
using SocialNode.Contracts.Dtos.Responses.ClusterOfPeople;

namespace SocialNode.Contracts.Queries;

public record GetClustersQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedClusterDto>;