using MediatR;
using TaskModule.Contracts.Commands;
using TaskModule.Contracts.Repositories;

namespace TaskModule.Application.Features.Commands;

public class DeleteTasksByWorkspaceCommandHandler : IRequestHandler<DeleteTasksByWorkspaceCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTasksByWorkspaceCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(DeleteTasksByWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.FindAsync(x => x.WorkspaceId == request.WorkspaceId);

        await _taskRepository.RemoveRangeAsync(tasks);

        return Unit.Value;
    }
}