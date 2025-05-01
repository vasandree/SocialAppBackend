using MediatR;
using TaskModule.Contracts.Dtos;
using TaskModule.Contracts.Dtos.Requests;

namespace TaskModule.Contracts.Commands;

public record CreateTaskCommand(Guid UserId, CreateTaskDto CreateTaskDto): IRequest<Unit>;