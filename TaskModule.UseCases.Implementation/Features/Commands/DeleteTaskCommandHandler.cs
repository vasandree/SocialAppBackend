using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.UseCases.Interfaces.Commands;

namespace TaskModule.UseCases.Implementation.Features.Commands;

internal sealed class DeleteTaskCommandHandler(ITaskRepository repository) : IRequestHandler<DeleteTaskCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await repository.CheckIfExists(request.TaskId))
            throw new NotFound("Task not found");

        if (!await repository.CheckIfBelongsToUserAsync(request.TaskId, request.UserId))
            throw new Forbidden("Task doesn't belong to user");

        await repository.DeleteByIdAsync(request.TaskId);
        
        await repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}