using MediatR;
using Workspace.Contracts.Dtos.Requests;

namespace Workspace.Contracts.Commands;

public record CreateWorkspaceCommand(Guid UserId, ShortenWorkspaceDto WorkspaceRequestDto) : IRequest<Guid>;