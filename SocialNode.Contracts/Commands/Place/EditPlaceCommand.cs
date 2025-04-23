using MediatR;
using SocialNode.Contracts.Dtos.Requests;

namespace SocialNode.Contracts.Commands.Place;

public record EditPlaceCommand(Guid UserId, Guid PlaceId, PlaceRequestDto PlaceRequestDto) : IRequest<Unit>;