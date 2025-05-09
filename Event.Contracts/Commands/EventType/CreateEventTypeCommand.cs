using Event.Contracts.Dtos.Requests;
using MediatR;

namespace Event.Contracts.Commands.EventType;

public record CreateEventTypeCommand(Guid UserId, EventTypeRequestDto EventTypeRequestDto) : IRequest<Unit>;