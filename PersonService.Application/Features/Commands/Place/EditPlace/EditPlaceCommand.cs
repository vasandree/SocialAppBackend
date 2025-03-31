using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.Place.EditPlace;

public record EditPlaceCommand(Guid UserId, Guid PlaceId, PlaceRequestDto PlaceRequestDto) : IRequest<Unit>;