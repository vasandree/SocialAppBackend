using MediatR;

namespace Event.Contracts.Commands.EventType;

public record DeleteEventTypeCommand(Guid UserId, Guid EventTypeId) : IRequest<Unit>;