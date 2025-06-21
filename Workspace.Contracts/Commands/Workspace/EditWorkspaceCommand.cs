using MediatR;
using Workspace.Contracts.Dtos.Requests;
using Workspace.Contracts.Dtos.Responses;

namespace Workspace.Contracts.Commands;

public record EditWorkspaceCommand(Guid UserId, Guid WorkspaceId, ShortenWorkspaceDto WorkspaceRequestDto)
    : IRequest<ListedWorkspaceDto>;