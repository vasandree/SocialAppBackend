using MediatR;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Commands.Workspace;

public record EditWorkspaceContentCommand(Guid UserId, Guid WorkspaceId, string Content) : IRequest<WorkspaceResponseDto>;