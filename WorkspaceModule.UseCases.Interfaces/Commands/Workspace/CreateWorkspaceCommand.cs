using MediatR;
using WorkspaceModule.UseCases.Interfaces.Dtos.Requests;

namespace WorkspaceModule.UseCases.Interfaces.Commands.Workspace;

public record CreateWorkspaceCommand(Guid UserId, UpdateWorkspaceDto WorkspaceRequestDto) : IRequest<Guid>;