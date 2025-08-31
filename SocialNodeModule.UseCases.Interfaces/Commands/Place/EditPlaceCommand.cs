using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNodeModule.UseCases.Interfaces.Commands.Place;

public record EditPlaceCommand(Guid UserId, Guid PlaceId, PlaceRequestDto PlaceRequestDto) : IRequest<Unit>;