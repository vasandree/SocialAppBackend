using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.Place;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.Places;

internal sealed class DeletePlaceCommandHandler(IPlaceRepository socialNodeRepository)
    : IRequestHandler<DeletePlaceCommand, Unit>
{
    public async Task<Unit> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        if (!await socialNodeRepository.CheckIfExists(request.PlaceId))
            throw new NotFound($"Place with id={request.PlaceId} not found");

        var place = await socialNodeRepository.GetByIdAsync(request.PlaceId);

        if (!place.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to delete");

        socialNodeRepository.DeleteAsync(place);
        
        await socialNodeRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}