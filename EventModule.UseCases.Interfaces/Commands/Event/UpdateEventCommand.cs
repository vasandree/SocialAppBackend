using EventModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;

namespace EventModule.UseCases.Interfaces.Commands.Event;

public record UpdateEventCommand(Guid UserId, Guid EventId, UpdateEventDto UpdateEventDto) : IRequest<Unit>;