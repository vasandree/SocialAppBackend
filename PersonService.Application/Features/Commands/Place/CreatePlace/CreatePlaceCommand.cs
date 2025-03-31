using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.Place.CreatePlace;

public record CreatePlaceCommand(Guid UserId, PlaceRequestDto PlaceRequestDto) : IRequest<Unit>;