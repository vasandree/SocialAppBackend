using MediatR;
using PersonService.Domain.Entities;
using PersonService.Infrastructure.CloudStorage;
using PersonService.Persistence.Repositories.ClusterRepository;

namespace PersonService.Application.Features.Commands.ClustersOfPeople.CreateClusterOfPeople;

public class CreateClusterCommandHandler : IRequestHandler<CreateClusterCommand, Unit>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public CreateClusterCommandHandler(IClusterRepository clusterRepository,
        ICloudStorageService cloudStorageService)
    {
        _clusterRepository = clusterRepository;
        _cloudStorageService = cloudStorageService;
    }

    public async Task<Unit> Handle(CreateClusterCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence

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