using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.Places.CreatePlace;

public record CreatePlaceCommand(Guid UserId, PlaceRequestDto PlaceRequestDto) : IRequest<Unit>;