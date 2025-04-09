using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.ClustersOfPeople.EditClusterOfPeople;

public record EditClusterCommand(Guid UserId, Guid ClusterId, ClusterRequestDto ClusterRequestDto) : IRequest<Unit>;