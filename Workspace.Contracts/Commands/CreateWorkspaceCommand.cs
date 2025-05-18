using MediatR;
using Workspace.Contracts.Dtos.Requests;

namespace Workspace.Contracts.Commands;

public record CreateWorkspaceCommand(Guid UserId, WorkspaceRequestDto WorkspaceRequestDto) : IRequest<Unit>;