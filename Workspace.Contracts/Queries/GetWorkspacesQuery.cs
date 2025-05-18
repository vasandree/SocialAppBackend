using MediatR;
using Workspace.Contracts.Dtos.Responses;

namespace Workspace.Contracts.Queries;

public record GetWorkspacesQuery(Guid UserId) : IRequest<WorkspacesDto>;