using MediatR;
using TaskModule.Contracts.Dtos.Responses;

namespace TaskModule.Contracts.Queries;

public record GetTasksQuery(Guid UserId) : IRequest<TasksDto>;