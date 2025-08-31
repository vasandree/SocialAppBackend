using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;

namespace SocialNodeModule.UseCases.Interfaces.Queries;

public record GetClustersQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedClusterDto>;