using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.Place;
using SocialNodeModule.UseCases.Interfaces.Services;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.Places;

internal sealed class EditPlaceCommandHandler(
    IPlaceRepository placeRepository,
    ICloudStorageService cloudStorageService)
    : IRequestHandler<EditPlaceCommand, Unit>
{
    public async Task<Unit> Handle(EditPlaceCommand request, CancellationToken cancellationToken)
    {
        if (!await placeRepository.CheckIfExists(request.PlaceId))
            throw new NotFound($"Place with id={request.PlaceId} not found");

        var place = await placeRepository.GetByIdAsync(request.PlaceId);

        if (!place.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to edit");

        var avatar = request.PlaceRequestDto.Avatar != null
            ? await cloudStorageService.UploadFileAsync(request.PlaceRequestDto.Avatar, place.Id)
            : null;

        place.EditInfo(request.PlaceRequestDto.Name, request.PlaceRequestDto.Description, avatar);

        await placeRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}