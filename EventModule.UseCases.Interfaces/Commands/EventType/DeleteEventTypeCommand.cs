using MediatR;

namespace EventModule.UseCases.Interfaces.Commands.EventType;

public record DeleteEventTypeCommand(Guid UserId, Guid EventTypeId) : IRequest<Unit>;