using MediatR;
using PersonService.Application.Dtos.Responses.ClusterOfPeople;

namespace PersonService.Application.Features.Queries.GetClustersQuery;

public record GetClustersQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedClusterDto>;