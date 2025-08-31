using MediatR;

namespace WorkspaceModule.UseCases.Interfaces.Commands.Relation;

public record DeleteRelationCommand(Guid UserId, Guid Id) : IRequest<Unit>;