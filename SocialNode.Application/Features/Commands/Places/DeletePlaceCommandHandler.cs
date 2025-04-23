using Common.Exceptions;
using MediatR;
using SocialNode.Contracts.Commands.Place;
using SocialNode.Contracts.Repositories;

namespace SocialNode.Application.Features.Commands.Places;

public class DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, Unit>
{
    private readonly IPlaceRepository _placeRepository;

    public DeletePlaceCommandHandler(IPlaceRepository socialNodeRepository)
    {
        _placeRepository = socialNodeRepository;
    }

    public async Task<Unit> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence
        
        if (!await _placeRepository.CheckIfExists(request.PlaceId))
            throw new NotFound($"Place with id={request.PlaceId} not found");
        
        var place = await _placeRepository.GetByIdAsync(request.PlaceId);
        
        if (place!.CreatorId != request.UserId) throw new Forbidden("You are not allowed to delete");
        
        await _placeRepository.DeleteAsync(place);
        
        return Unit.Value;
    }
}