using MediatR;
using TaskModule.Domain.Enums;

namespace TaskModule.UseCases.Interfaces.Commands;

public record ChangeTaskStatusCommand(Guid UserId, Guid TaskId, StatusOfTask Status) : IRequest<Unit>;