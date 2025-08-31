using MediatR;

namespace WorkspaceModule.UseCases.Interfaces.Commands.Workspace;

public record DeleteWorkspaceCommand(Guid UserId, Guid WorkspaceId): IRequest<Unit>;