using MediatR;
using TaskModule.UseCases.Interfaces.Dtos.Requests;

namespace TaskModule.UseCases.Interfaces.Commands;

public record EditTaskCommand(Guid UserId, Guid TaskId, CreateTaskDto CreateTaskDto) : IRequest<Unit>;