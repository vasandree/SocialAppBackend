using MediatR;
using SocialNode.Contracts.Dtos.Requests;

namespace SocialNode.Contracts.Commands.Place;

public record CreatePlaceCommand(Guid UserId, PlaceRequestDto PlaceRequestDto) : IRequest<Unit>;