using MediatR;
using TaskModule.Contracts.Commands;
using TaskModule.Contracts.Repositories;
using TaskModule.Domain.Entites;

namespace TaskModule.Application.Features.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Unit>
{
    private readonly ITaskRepository _repository;

    public CreateTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        //todo: check workspace 
        
        await _repository.AddAsync(new TaskEntity
        {
            Name = request.CreateTaskDto.Name,
            Description = request.CreateTaskDto.Description,
            CreateDate = request.CreateTaskDto.StartDate,
            EndDate = request.CreateTaskDto.EndDate,
            SocialNodeId = request.CreateTaskDto.SocialNodeId,
            CreatorId = request.UserId
        });

        return Unit.Value;
    }
}