using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.Places.DeletePlace;

public record DeletePlaceCommand(Guid UserId, Guid PlaceId) : IRequest<Unit>;