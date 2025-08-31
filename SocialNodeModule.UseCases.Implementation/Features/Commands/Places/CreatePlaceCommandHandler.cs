using MediatR;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.Place;
using SocialNodeModule.UseCases.Interfaces.Services;
using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.Places;

internal sealed class CreatePlaceCommandHandler(
    IPlaceRepository placeRepository,
    ICloudStorageService cloudStorageService)
    : IRequestHandler<CreatePlaceCommand, Unit>
{
    public async Task<Unit> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var avatar = request.PlaceRequestDto.Avatar != null
            ? await cloudStorageService.UploadFileAsync(request.PlaceRequestDto.Avatar, id)
            : null;

        await placeRepository.AddAsync(new Place(id, request.PlaceRequestDto.Name, request.PlaceRequestDto.Description,
            avatar, request.UserId));

        await placeRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}