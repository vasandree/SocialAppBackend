using MediatR;
using TaskModule.UseCases.Interfaces.Dtos;
using TaskModule.UseCases.Interfaces.Dtos.Requests;

namespace TaskModule.UseCases.Interfaces.Commands;

public record CreateTaskCommand(Guid UserId, CreateTaskDto CreateTaskDto): IRequest<Unit>;