using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using Workspace.Contracts.Commands.Relation;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Commands.Relation;

public class UpdateRelationCommandHandler : IRequestHandler<UpdateRelationCommand, RelationDto>
{
    private readonly IMapper _mapper;
    private readonly IRelationRepository _repository;

    public UpdateRelationCommandHandler(IMapper mapper, IRelationRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<RelationDto> Handle(UpdateRelationCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.CheckIfExists(request.Id))
            throw new NotFound("Relation not found");

        var relation = await _repository.GetByIdAsync(request.Id);

        if (relation.CreatorId != request.UserId)
            throw new Forbidden("You are not allowed to update this relation");

        relation.Name = request.Dto.Name;
        relation.Description = request.Dto.Description;
        relation.Color = request.Dto.Color;

        await _repository.UpdateAsync(relation);

        return _mapper.Map<RelationDto>(relation);
    }
}