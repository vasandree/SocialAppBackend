using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.Contracts.Commands;
using TaskModule.Contracts.Repositories;

namespace TaskModule.Application.Features.Commands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly ITaskRepository _repository;

    public DeleteTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.CheckIfExists(request.TaskId))
            throw new NotFound("Task not found");

        if (!await _repository.CheckIfBelongsToUserAsync(request.TaskId, request.UserId))
            throw new Forbidden("Task doesn't belong to user");

        await _repository.DeleteByIdAsync(request.TaskId);

        return Unit.Value;
    }
}