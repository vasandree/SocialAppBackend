using MediatR;

namespace TaskModule.UseCases.Interfaces.Commands;

public record DeleteTaskCommand(Guid UserId, Guid TaskId) : IRequest<Unit>;