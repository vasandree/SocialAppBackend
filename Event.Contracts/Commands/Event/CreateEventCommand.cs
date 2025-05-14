using Event.Contracts.Dtos.Requests;
using MediatR;

namespace Event.Contracts.Commands.Event;

public record CreateEventCommand(Guid UserId, CreateEventDto CreateEventDto) : IRequest<Unit>;