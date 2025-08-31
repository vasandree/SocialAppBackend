using MediatR;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.ClusterOfPeople;
using SocialNodeModule.UseCases.Interfaces.Services;
using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.ClustersOfPeople;

internal sealed class CreateClusterCommandHandler(
    IClusterRepository clusterRepository,
    ICloudStorageService cloudStorageService)
    : IRequestHandler<CreateClusterCommand, Unit>
{
    public async Task<Unit> Handle(CreateClusterCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var avatarUrl = request.ClusterRequestDto.Avatar != null
            ? await cloudStorageService.UploadFileAsync(request.ClusterRequestDto.Avatar, id)
            : null;

        await clusterRepository.AddAsync(new ClusterOfPeople(id, request.ClusterRequestDto.Name,
            request.ClusterRequestDto.Description, avatarUrl, request.UserId));

        await clusterRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}