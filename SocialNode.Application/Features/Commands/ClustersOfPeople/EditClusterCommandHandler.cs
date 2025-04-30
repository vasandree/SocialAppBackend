using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Commands.ClusterOfPeople;
using SocialNode.Contracts.Dtos.Requests;
using SocialNode.Contracts.Repositories;
using SocialNode.Contracts.Services;
using SocialNode.Domain.Entities;
using User.Contracts.Repositories;

namespace SocialNode.Application.Features.Commands.ClustersOfPeople;

public class EditClusterCommandHandler : IRequestHandler<EditClusterCommand, Unit>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public EditClusterCommandHandler(IClusterRepository clusterRepository,
        ICloudStorageService cloudStorageService, IUserRepository userRepository)
    {
        _clusterRepository = clusterRepository;
        _cloudStorageService = cloudStorageService;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(EditClusterCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExists(request.UserId))
            throw new BadRequest("User does not exist");

        if (!await _clusterRepository.CheckIfExists(request.ClusterId))
            throw new NotFound($"Cluster with id={request.ClusterId} not found");

        var cluster = await _clusterRepository.GetByIdAsync(request.ClusterId);

        if (cluster!.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit");

        await EditCluster(cluster, request.ClusterRequestDto);

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