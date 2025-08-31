using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNodeModule.UseCases.Interfaces.Commands.ClusterOfPeople;

public record EditClusterCommand(Guid UserId, Guid ClusterId, ClusterRequestDto ClusterRequestDto) : IRequest<Unit>;