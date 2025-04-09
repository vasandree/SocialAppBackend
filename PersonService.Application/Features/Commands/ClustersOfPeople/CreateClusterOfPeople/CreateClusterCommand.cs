using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.ClustersOfPeople.CreateClusterOfPeople;

public record CreateClusterCommand(Guid UserId, ClusterRequestDto ClusterRequestDto):IRequest<Unit>;