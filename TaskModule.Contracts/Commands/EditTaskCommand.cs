using MediatR;
using TaskModule.Contracts.Dtos.Requests;

namespace TaskModule.Contracts.Commands;

public record EditTaskCommand(Guid UserId, Guid TaskId, CreateTaskDto CreateTaskDto) : IRequest<Unit>;