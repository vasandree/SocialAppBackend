using Common.Exceptions;
using MediatR;
using SocialNode.Contracts.Commands.Place;
using SocialNode.Contracts.Dtos.Requests;
using SocialNode.Contracts.Repositories;
using SocialNode.Contracts.Services;
using SocialNode.Domain.Entities;

namespace SocialNode.Application.Features.Commands.Places;

public class EditPlaceCommandHandler : IRequestHandler<EditPlaceCommand, Unit>
{
    private readonly IPlaceRepository _placeRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public EditPlaceCommandHandler(IPlaceRepository placeRepository,
        ICloudStorageService cloudStorageService)
    {
        _placeRepository = placeRepository;
        _cloudStorageService = cloudStorageService;
    }

    public async Task<Unit> Handle(EditPlaceCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence

        if (!await _placeRepository.CheckIfExists(request.PlaceId))
            throw new NotFound($"Place with id={request.PlaceId} not found");

        var place = await _placeRepository.GetByIdAsync(request.PlaceId);

        if (place!.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit");

        EditPlace(place, request.PlaceRequestDto);

        await _placeRepository.UpdateAsync(place);

        return Unit.Value;
    }

    private void EditPlace(Place place, PlaceRequestDto placeRequestDto)
    {
        place.Name = placeRequestDto.Name;
        place.Description = placeRequestDto.Description;
        place.AvatarUrl = placeRequestDto.Avatar != null
            ? _cloudStorageService.UploadFileAsync(placeRequestDto.Avatar, place.Id).Result
            : place.AvatarUrl;
    }
}