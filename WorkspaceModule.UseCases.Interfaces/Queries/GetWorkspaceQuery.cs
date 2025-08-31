using MediatR;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Queries;

public record GetWorkspaceQuery(Guid UserId, Guid WorkspaceId) : IRequest<WorkspaceResponseDto>;