using MediatR;

namespace EventModule.UseCases.Interfaces.Commands.Event;

public record DeleteEventCommand(Guid UserId, Guid EventId) : IRequest<Unit>;