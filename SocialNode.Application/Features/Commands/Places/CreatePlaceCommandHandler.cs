using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Commands.Place;
using SocialNode.Contracts.Repositories;
using SocialNode.Contracts.Services;
using SocialNode.Domain.Entities;
using User.Contracts.Repositories;

namespace SocialNode.Application.Features.Commands.Places;

public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, Unit>
{
    private readonly IPlaceRepository _placeRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public CreatePlaceCommandHandler(IPlaceRepository placeRepository,
        ICloudStorageService cloudStorageService, IUserRepository userRepository)
    {
        _placeRepository = placeRepository;
        _cloudStorageService = cloudStorageService;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
    {
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