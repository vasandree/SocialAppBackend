using MediatR;

namespace Workspace.Contracts.Commands;

public record DeleteWorkspaceCommand(Guid UserId, Guid WorkspaceId): IRequest<Unit>;