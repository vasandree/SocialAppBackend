using MediatR;
using TaskModule.UseCases.Interfaces.Dtos.Responses;

namespace TaskModule.UseCases.Interfaces.Queries;

public record GetTasksQuery(Guid UserId) : IRequest<TasksDto>;