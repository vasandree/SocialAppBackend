using EventModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;

namespace EventModule.UseCases.Interfaces.Commands.EventType;

public record EditEventTypeCommand(Guid UserId, Guid EventTypeId, EventTypeRequestDto EventTypeRequestDto)
    : IRequest<Unit>;