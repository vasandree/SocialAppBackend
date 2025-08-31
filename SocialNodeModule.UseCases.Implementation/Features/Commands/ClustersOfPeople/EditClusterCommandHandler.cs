using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.ClusterOfPeople;
using SocialNodeModule.UseCases.Interfaces.Services;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.ClustersOfPeople;

internal sealed class EditClusterCommandHandler(
    IClusterRepository clusterRepository,
    ICloudStorageService cloudStorageService)
    : IRequestHandler<EditClusterCommand, Unit>
{
    public async Task<Unit> Handle(EditClusterCommand request, CancellationToken cancellationToken)
    {
        if (!await clusterRepository.CheckIfExists(request.ClusterId))
            throw new NotFound($"Cluster with id={request.ClusterId} not found");

        var cluster = await clusterRepository.GetByIdAsync(request.ClusterId);

        if (!cluster.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to edit");

        var avatar = request.ClusterRequestDto.Avatar != null
            ? await cloudStorageService.UploadFileAsync(request.ClusterRequestDto.Avatar, cluster.Id)
            : null;
        
        cluster.EditInfo(request.ClusterRequestDto.Name, request.ClusterRequestDto.Description, avatar);
        
        await clusterRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}