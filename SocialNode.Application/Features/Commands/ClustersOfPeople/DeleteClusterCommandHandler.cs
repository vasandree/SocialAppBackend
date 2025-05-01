using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Commands.ClusterOfPeople;
using SocialNode.Contracts.Repositories;
using User.Contracts.Repositories;

namespace SocialNode.Application.Features.Commands.ClustersOfPeople;

public class DeleteClusterCommandHandler : IRequestHandler<DeleteClusterCommand, Unit>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly IUserRepository _userRepository;

    public DeleteClusterCommandHandler(IClusterRepository clusterRepository, IUserRepository userRepository)
    {
        _clusterRepository = clusterRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteClusterCommand request, CancellationToken cancellationToken)
    {
        if (!await _clusterRepository.CheckIfExists(request.ClusterId))
            throw new NotFound($"Cluster with id={request.ClusterId} not found");

        var cluster = await _clusterRepository.GetByIdAsync(request.ClusterId);

        if (cluster!.CreatorId != request.UserId) throw new Forbidden("You are not allowed to delete");

        await _clusterRepository.DeleteAsync(cluster);

        return Unit.Value;
    }
}