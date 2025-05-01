using MediatR;
using TaskModule.Contracts.Dtos.Responses;

namespace TaskModule.Contracts.Queries;

public record GetTaskByIdQuery(Guid UserId, Guid TaskId) : IRequest<TaskDto>;