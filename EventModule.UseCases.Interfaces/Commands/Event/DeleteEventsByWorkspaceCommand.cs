using MediatR;

namespace EventModule.UseCases.Interfaces.Commands.Event;

public record DeleteEventsByWorkspaceCommand(Guid WorkspaceId) : IRequest<Unit>;