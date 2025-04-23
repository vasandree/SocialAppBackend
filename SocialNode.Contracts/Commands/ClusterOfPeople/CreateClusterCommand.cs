using MediatR;
using SocialNode.Contracts.Dtos.Requests;

namespace SocialNode.Contracts.Commands.ClusterOfPeople;

public record CreateClusterCommand(Guid UserId, ClusterRequestDto ClusterRequestDto) : IRequest<Unit>;