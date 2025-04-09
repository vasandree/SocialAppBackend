using Common.Exceptions;
using MediatR;
using PersonService.Application.Dtos.Requests;
using PersonService.Domain.Entities;
using PersonService.Infrastructure.CloudStorage;
using PersonService.Persistence.Repositories.ClusterRepository;

namespace PersonService.Application.Features.Commands.ClustersOfPeople.EditClusterOfPeople;

public class EditClusterCommandHandler : IRequestHandler<EditClusterCommand, Unit>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public EditClusterCommandHandler(IClusterRepository clusterRepository,
        ICloudStorageService cloudStorageService)
    {
        _clusterRepository = clusterRepository;
        _cloudStorageService = cloudStorageService;
    }

    public async Task<Unit> Handle(EditClusterCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence

        if (!await _clusterRepository.CheckIfExists(request.ClusterId))
            throw new NotFound($"Cluster with id={request.ClusterId} not found");

        var cluster = await _clusterRepository.GetByIdAsync(request.ClusterId);

        if (cluster!.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit");

        EditCluster(cluster, request.ClusterRequestDto);

        await _clusterRepository.UpdateAsync(cluster);

        return Unit.Value;
    }

    private async Task EditCluster(ClusterOfPeople cluster, ClusterRequestDto clusterDto)
    {
        cluster.Name = clusterDto.Name;
        cluster.Description = clusterDto.Description ?? cluster.Description;
        cluster.AvatarUrl = clusterDto.Avatar != null
            ? await _cloudStorageService.UploadFileAsync(clusterDto.Avatar, cluster.Id)
            : cluster.AvatarUrl;
    }
}