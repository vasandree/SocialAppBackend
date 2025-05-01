using MediatR;
using TaskModule.Domain.Enums;

namespace TaskModule.Contracts.Commands;

public record ChangeTaskStatusCommand(Guid UserId, Guid TaskId, StatusOfTask Status) : IRequest<Unit>;