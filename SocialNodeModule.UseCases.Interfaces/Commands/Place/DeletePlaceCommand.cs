using MediatR;

namespace SocialNodeModule.UseCases.Interfaces.Commands.Place;

public record DeletePlaceCommand(Guid UserId, Guid PlaceId) : IRequest<Unit>;