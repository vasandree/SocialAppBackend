using MediatR;
using PersonService.Application.Dtos.Responses;

namespace PersonService.Application.Features.Queries.GetClusterQuery;

public record GetClusterQuery(Guid ClusterId) : IRequest<ClusterOfPeopleDto>;