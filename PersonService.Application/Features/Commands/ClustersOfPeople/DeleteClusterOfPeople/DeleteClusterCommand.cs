using MediatR;

namespace PersonService.Application.Features.Commands.ClustersOfPeople.DeleteClusterOfPeople;

public record DeleteClusterCommand(Guid UserId, Guid ClusterId): IRequest<Unit>;