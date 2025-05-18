using MediatR;

namespace Workspace.Contracts.Commands;

public record EditWorkspaceContentCommand(Guid UserId, Guid WorkspaceId, string Content) : IRequest<bool>;