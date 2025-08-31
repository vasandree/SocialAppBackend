using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNodeModule.UseCases.Interfaces.Commands.Place;

public record CreatePlaceCommand(Guid UserId, PlaceRequestDto PlaceRequestDto) : IRequest<Unit>;