using MediatR;

namespace TaskModule.Contracts.Commands;

public record DeleteTasksByWorkspaceCommand(Guid WorkspaceId) : IRequest<Unit>;