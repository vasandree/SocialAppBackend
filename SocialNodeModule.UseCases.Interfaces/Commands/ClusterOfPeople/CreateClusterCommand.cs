using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNodeModule.UseCases.Interfaces.Commands.ClusterOfPeople;

public record CreateClusterCommand(Guid UserId, ClusterRequestDto ClusterRequestDto) : IRequest<Unit>;