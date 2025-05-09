using Event.Contracts.Dtos.Requests;
using MediatR;

namespace Event.Contracts.Commands.EventType;

public record EditEventTypeCommand(Guid UserId, Guid EventTypeId, EventTypeRequestDto EventTypeRequestDto)
    : IRequest<Unit>;