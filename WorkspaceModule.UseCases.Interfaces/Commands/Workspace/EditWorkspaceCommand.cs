using MediatR;
using WorkspaceModule.UseCases.Interfaces.Dtos.Requests;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Commands.Workspace;

public record EditWorkspaceCommand(Guid UserId, Guid WorkspaceId, ShortenWorkspaceDto WorkspaceRequestDto)
    : IRequest<ListedWorkspaceDto>;