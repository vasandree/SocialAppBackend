using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.Place.DeletePlace;

public record DeletePlaceCommand(Guid UserId, Guid PlaceId, PlaceRequestDto PlaceRequestDto) : IRequest<Unit>;