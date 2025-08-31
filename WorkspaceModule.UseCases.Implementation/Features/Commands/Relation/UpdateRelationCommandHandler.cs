using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Commands.Relation;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Implementation.Features.Commands.Relation;

internal sealed class UpdateRelationCommandHandler(IMapper mapper, IRelationRepository repository)
    : IRequestHandler<UpdateRelationCommand, RelationDto>
{
    public async Task<RelationDto> Handle(UpdateRelationCommand request, CancellationToken cancellationToken)
    {
        if (!await repository.CheckIfExists(request.Id))
            throw new NotFound("Relation not found");

        var relation = await repository.GetByIdAsync(request.Id);

        if (!relation.IsUserCreator(request.UserId))
            throw new Forbidden("You are not allowed to update this relation");

        relation.UpdateInfo(request.Dto.Name,request.Dto.Description,request.Dto.Color);
        
        await repository.SaveChangesAsync(cancellationToken);

        return mapper.Map<RelationDto>(relation);
    }
}