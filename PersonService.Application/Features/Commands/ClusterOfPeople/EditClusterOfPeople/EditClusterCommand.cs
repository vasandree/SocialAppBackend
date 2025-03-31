using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.ClusterOfPeople.EditClusterOfPeople;

public record EditClusterCommand(Guid UserId, Guid ClusterId, ClusterRequestDto ClusterRequestDto) : IRequest<Unit>;