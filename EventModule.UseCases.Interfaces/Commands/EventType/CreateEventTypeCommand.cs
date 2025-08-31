using EventModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;

namespace EventModule.UseCases.Interfaces.Commands.EventType;

public record CreateEventTypeCommand(Guid UserId, EventTypeRequestDto EventTypeRequestDto) : IRequest<Unit>;