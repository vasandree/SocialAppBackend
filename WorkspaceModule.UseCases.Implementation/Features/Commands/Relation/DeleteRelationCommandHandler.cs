using MediatR;
using Shared.Domain.Exceptions;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Commands.Relation;

namespace WorkspaceModule.UseCases.Implementation.Features.Commands.Relation;

public class DeleteRelationCommandHandler(IRelationRepository repository) : IRequestHandler<DeleteRelationCommand, Unit>
{
    public async Task<Unit> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
    {
        if (!await repository.CheckIfExists(request.Id))
            throw new NotFound("Relation not found");

        var relation = await repository.GetByIdAsync(request.Id);

        if (!relation.IsUserCreator(request.UserId))
            throw new Forbidden("You are not allowed to delete this relation");

        repository.DeleteAsync(relation);

        await repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}