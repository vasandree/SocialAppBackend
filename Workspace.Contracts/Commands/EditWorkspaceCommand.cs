using MediatR;
using Workspace.Contracts.Dtos.Requests;

namespace Workspace.Contracts.Commands;

public record EditWorkspaceCommand(Guid UserId, Guid workspaceId, WorkspaceRequestDto WorkspaceRequestDto)
    : IRequest<Unit>;