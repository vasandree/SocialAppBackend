using Event.Contracts.Dtos.Requests;
using MediatR;

namespace Event.Contracts.Commands.Event;

public record UpdateEventCommand(Guid UserId, Guid EventId, UpdateEventDto UpdateEventDto) : IRequest<Unit>;