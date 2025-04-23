using MediatR;

namespace SocialNode.Contracts.Commands.ClusterOfPeople;

public record DeleteClusterCommand(Guid UserId, Guid ClusterId) : IRequest<Unit>;