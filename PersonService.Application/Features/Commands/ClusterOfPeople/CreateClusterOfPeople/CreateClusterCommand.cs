using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.ClusterOfPeople.CreateClusterOfPeople;

public record CreateClusterCommand(Guid UserId, ClusterRequestDto ClusterRequestDto):IRequest<Unit>;