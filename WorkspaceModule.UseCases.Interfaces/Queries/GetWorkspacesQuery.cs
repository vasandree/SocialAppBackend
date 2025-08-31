using MediatR;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Queries;

public record GetWorkspacesQuery(Guid UserId, int Page) : IRequest<WorkspacesDto>;