using MediatR;

namespace Workspace.Contracts.Commands.Relation;

public record DeleteRelationCommand(Guid UserId, Guid Id) : IRequest<Unit>;