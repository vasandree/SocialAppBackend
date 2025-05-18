using MediatR;
using Workspace.Contracts.Dtos.Responses;

namespace Workspace.Contracts.Queries;

public record GetWorkspaceQuery(Guid UserId, Guid WorkspaceId) : IRequest<WorkspaceResponseDto>;