using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.UseCases.Interfaces.Commands;
using TaskModule.Domain.Entites;
using WorkspaceModule.DataAccess.Interfaces.Repositories;

namespace TaskModule.UseCases.Implementation.Features.Commands;

internal sealed class CreateTaskCommandHandler(
    ITaskRepository repository,
    IWorkspaceEntityRepository workspaceRepository)
    : IRequestHandler<CreateTaskCommand, Unit>
{
    public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await workspaceRepository.CheckIfExists(request.CreateTaskDto.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await workspaceRepository.GetByIdAsync(request.CreateTaskDto.WorkspaceId);
        
        var task = new TaskEntity(request.CreateTaskDto.Name, request.CreateTaskDto.Description,
            request.CreateTaskDto.StartDate, request.CreateTaskDto.EndDate, request.CreateTaskDto.SocialNodeId,
            request.UserId, workspace.Id);

        workspace.Tasks.Add(task.Id);

        await repository.AddAsync(task);

        await repository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}