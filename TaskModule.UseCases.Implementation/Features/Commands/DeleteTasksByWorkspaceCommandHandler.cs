using MediatR;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.UseCases.Interfaces.Commands;

namespace TaskModule.UseCases.Implementation.Features.Commands;

internal sealed class DeleteTasksByWorkspaceCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<DeleteTasksByWorkspaceCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTasksByWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.FindAsync(x => x.WorkspaceId == request.WorkspaceId);

        taskRepository.RemoveRangeAsync(tasks);

        await taskRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}