using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Queries;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Queries;

public class GetRelationQueryHandler : IRequestHandler<GetRelationQuery, RelationDto>
{
    private readonly IMapper _mapper;
    private readonly IRelationRepository _repository;

    public GetRelationQueryHandler(IRelationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RelationDto> Handle(GetRelationQuery request, CancellationToken cancellationToken)
    {
        if (!await _repository.CheckIfExists(request.Id))
            throw new NotFound("Relation not found");

        var relation = await _repository.GetByIdAsync(request.Id);

        if (relation.CreatorId != request.UserId)
            throw new Forbidden("You are not allowed to access this relation");

        return _mapper.Map<RelationDto>(relation);
    }
}