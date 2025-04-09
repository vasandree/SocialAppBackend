using MediatR;
using PersonService.Domain.Entities;
using PersonService.Infrastructure.CloudStorage;
using PersonService.Persistence.Repositories.PlaceRepository;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Application.Features.Commands.Places.CreatePlace;

public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, Unit>
{
    private readonly IPlaceRepository _placeRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public CreatePlaceCommandHandler(IPlaceRepository placeRepository,
        ICloudStorageService cloudStorageService)
    {
        _placeRepository = placeRepository;
        _cloudStorageService = cloudStorageService;
    }

    public async Task<Unit> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence

        var id = Guid.NewGuid();
        await _placeRepository.AddAsync(new Place
        {
            Id = id,
            Description = request.PlaceRequestDto.Description,
            AvatarUrl = request.PlaceRequestDto.Avatar != null
                ? await _cloudStorageService.UploadFileAsync(request.PlaceRequestDto.Avatar, id)
                : null,
            Name = request.PlaceRequestDto.Name,
            CreatorId = request.UserId
        });

        return Unit.Value;
    }
}