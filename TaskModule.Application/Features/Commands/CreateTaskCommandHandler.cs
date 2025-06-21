using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.Contracts.Commands;
using TaskModule.Contracts.Repositories;
using TaskModule.Domain.Entites;
using Workspace.Contracts.Repositories;

namespace TaskModule.Application.Features.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Unit>
{
    private readonly ITaskRepository _repository;
    private readonly IWorkspaceEntityRepository _workspaceRepository;

    public CreateTaskCommandHandler(ITaskRepository repository, IWorkspaceEntityRepository workspaceRepository)
    {
        _repository = repository;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await _workspaceRepository.CheckIfExists(request.CreateTaskDto.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await _workspaceRepository.GetByIdAsync(request.CreateTaskDto.WorkspaceId);


        var task = new TaskEntity
        {
            Name = request.CreateTaskDto.Name,
            Description = request.CreateTaskDto.Description,
            CreateDate = request.CreateTaskDto.StartDate,
            EndDate = request.CreateTaskDto.EndDate,
            SocialNodeId = request.CreateTaskDto.SocialNodeId,
            CreatorId = request.UserId,
            WorkspaceId = workspace.Id
        };

        workspace.Tasks.Add(task.Id);

        await _repository.AddAsync(task);
        await _workspaceRepository.UpdateAsync(workspace);

        return Unit.Value;
    }
}