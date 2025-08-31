using EventModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;

namespace EventModule.UseCases.Interfaces.Commands.Event;

public record CreateEventCommand(Guid UserId, CreateEventDto CreateEventDto) : IRequest<Unit>;