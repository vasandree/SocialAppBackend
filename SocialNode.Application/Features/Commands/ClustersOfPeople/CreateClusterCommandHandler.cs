using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Commands.ClusterOfPeople;
using SocialNode.Contracts.Repositories;
using SocialNode.Contracts.Services;
using SocialNode.Domain.Entities;
using User.Contracts.Repositories;

namespace SocialNode.Application.Features.Commands.ClustersOfPeople;

public class CreateClusterCommandHandler : IRequestHandler<CreateClusterCommand, Unit>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public CreateClusterCommandHandler(IClusterRepository clusterRepository,
        ICloudStorageService cloudStorageService, IUserRepository userRepository)
    {
        _clusterRepository = clusterRepository;
        _cloudStorageService = cloudStorageService;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(CreateClusterCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        await _clusterRepository.AddAsync(new ClusterOfPeople
        {
            Id = id,
            Name = request.ClusterRequestDto.Name,
            Description = request.ClusterRequestDto.Description,
            AvatarUrl = request.ClusterRequestDto.Avatar != null
                ? await _cloudStorageService.UploadFileAsync(request.ClusterRequestDto.Avatar, id)
                : null,
            CreatorId = request.UserId
        });

        return Unit.Value;
    }
}