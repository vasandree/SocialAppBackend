using MediatR;

namespace TaskModule.UseCases.Interfaces.Commands;

public record DeleteTasksByWorkspaceCommand(Guid WorkspaceId) : IRequest<Unit>;