using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Commands.Place;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using User.Contracts.Repositories;

namespace SocialNode.Application.Features.Commands.Places;

public class DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPlaceRepository _placeRepository;

    public DeletePlaceCommandHandler(IPlaceRepository socialNodeRepository, IUserRepository userRepository)
    {
        _placeRepository = socialNodeRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        if (!await _placeRepository.CheckIfExists(request.PlaceId))
            throw new NotFound($"Place with id={request.PlaceId} not found");

        var place = await _placeRepository.GetByIdAsync(request.PlaceId);

        if (place!.CreatorId != request.UserId) throw new Forbidden("You are not allowed to delete");

        await _placeRepository.DeleteAsync(place);

        return Unit.Value;
    }
}