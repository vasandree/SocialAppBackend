using MediatR;

namespace SocialNode.Contracts.Commands.Place;

public record DeletePlaceCommand(Guid UserId, Guid PlaceId) : IRequest<Unit>;