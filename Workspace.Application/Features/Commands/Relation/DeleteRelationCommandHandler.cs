using MediatR;
using Shared.Domain.Exceptions;
using Workspace.Contracts.Commands.Relation;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Commands.Relation;

public class DeleteRelationCommandHandler : IRequestHandler<DeleteRelationCommand, Unit>
{
    private readonly IRelationRepository _repository;

    public DeleteRelationCommandHandler(IRelationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.CheckIfExists(request.Id))
            throw new NotFound("Relation not found");

        var relation = await _repository.GetByIdAsync(request.Id);

        if (relation.CreatorId != request.UserId)
            throw new Forbidden("You are not allowed to delete this relation");

        await _repository.DeleteAsync(relation);

        return Unit.Value;
    }
}