using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.ClusterOfPeople;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.ClustersOfPeople;

internal sealed class DeleteClusterCommandHandler(IClusterRepository clusterRepository)
    : IRequestHandler<DeleteClusterCommand, Unit>
{
    public async Task<Unit> Handle(DeleteClusterCommand request, CancellationToken cancellationToken)
    {
        if (!await clusterRepository.CheckIfExists(request.ClusterId))
            throw new NotFound($"Cluster with id={request.ClusterId} not found");

        var cluster = await clusterRepository.GetByIdAsync(request.ClusterId);

        if (!cluster.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to delete");

        clusterRepository.DeleteAsync(cluster);
        
        await clusterRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}