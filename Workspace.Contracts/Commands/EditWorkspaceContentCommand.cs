using MediatR;
using Workspace.Contracts.Dtos.Responses;

namespace Workspace.Contracts.Commands;

public record EditWorkspaceContentCommand(Guid UserId, Guid WorkspaceId, string Content) : IRequest<WorkspaceResponseDto>;