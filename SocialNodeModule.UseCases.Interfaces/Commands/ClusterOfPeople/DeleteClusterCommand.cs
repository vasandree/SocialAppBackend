using MediatR;

namespace SocialNodeModule.UseCases.Interfaces.Commands.ClusterOfPeople;

public record DeleteClusterCommand(Guid UserId, Guid ClusterId) : IRequest<Unit>;