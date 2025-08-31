using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.UseCases.Interfaces.Queries;

namespace WorkspaceModule.UseCases.Implementation.Features.Queries;

internal sealed class GetRelationQueryHandler(IRelationRepository repository, IMapper mapper)
    : IRequestHandler<GetRelationQuery, RelationDto>
{
    public async Task<RelationDto> Handle(GetRelationQuery request, CancellationToken cancellationToken)
    {
        if (!await repository.CheckIfExists(request.Id))
            throw new NotFound("Relation not found");

        var relation = await repository.GetByIdAsync(request.Id);

        if (!relation.IsUserCreator(request.UserId))
            throw new Forbidden("You are not allowed to access this relation");

        return mapper.Map<RelationDto>(relation);
    }
}