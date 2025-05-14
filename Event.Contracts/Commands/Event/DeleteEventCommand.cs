using MediatR;

namespace Event.Contracts.Commands.Event;

public record DeleteEventCommand(Guid UserId, Guid EventId) : IRequest<Unit>;