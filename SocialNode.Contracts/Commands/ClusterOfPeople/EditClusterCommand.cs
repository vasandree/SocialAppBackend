using MediatR;
using SocialNode.Contracts.Dtos.Requests;

namespace SocialNode.Contracts.Commands.ClusterOfPeople;

public record EditClusterCommand(Guid UserId, Guid ClusterId, ClusterRequestDto ClusterRequestDto) : IRequest<Unit>;