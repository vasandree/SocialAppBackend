using MediatR;

namespace TaskModule.Contracts.Commands;

public record DeleteTaskCommand(Guid UserId, Guid TaskId) : IRequest<Unit>;