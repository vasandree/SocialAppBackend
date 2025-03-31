using MediatR;

namespace PersonService.Application.Features.Commands.ClusterOfPeople.DeleteClusterOfPeople;

public record DeleteClusterCommand(Guid UserId, Guid ClusterId): IRequest<Unit>;