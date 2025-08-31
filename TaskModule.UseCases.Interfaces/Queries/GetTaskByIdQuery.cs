using MediatR;
using TaskModule.UseCases.Interfaces.Dtos.Responses;

namespace TaskModule.UseCases.Interfaces.Queries;

public record GetTaskByIdQuery(Guid UserId, Guid TaskId) : IRequest<TaskDto>;