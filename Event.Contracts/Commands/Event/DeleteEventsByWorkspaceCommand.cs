using MediatR;

namespace Event.Contracts.Commands.Event;

public record DeleteEventsByWorkspaceCommand(Guid WorkspaceId) : IRequest<Unit>;