using MediatR;
using PersonService.Application.Dtos.Responses.ClusterOfPeople;

namespace PersonService.Application.Features.Queries.GetClusterQuery;

public record GetClusterQuery(Guid ClusterId, Guid UserId) : IRequest<ClusterOfPeopleDto>;