using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.Domain.Entities;
using WorkspaceModule.UseCases.Interfaces.Commands.Relation;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.Domain.Entities;

namespace WorkspaceModule.UseCases.Implementation.Features.Commands.Relation;

internal sealed class CreateRelationCommandHandler(
    IMapper mapper,
    IRelationRepository relationRepository,
    IBaseSocialNodeRepository<BaseSocialNode> baseSocialNodeRepository,
    IWorkspaceEntityRepository workspaceEntityRepository)
    : IRequestHandler<CreateRelationCommand, RelationDto>
{
    public async Task<RelationDto> Handle(CreateRelationCommand request, CancellationToken cancellationToken)
    {
        if (!await baseSocialNodeRepository.CheckIfExists(request.Dto.FirstUnit) ||
            !await baseSocialNodeRepository.CheckIfExists(request.Dto.SecondUnit))
            throw new BadRequest("Invalid social node IDs provided for relation creation.");

        if (!await baseSocialNodeRepository.CheckIfUserIsCreator(request.UserId, request.Dto.FirstUnit) ||
            !await baseSocialNodeRepository.CheckIfUserIsCreator(request.UserId, request.Dto.SecondUnit))
            throw new Forbidden("You are not the creator of one or both social nodes involved in this relation.");

        var relation = new RelationEntity(request.Dto.Name, request.Dto.Description, request.Dto.Color,
            request.Dto.FirstUnit, request.Dto.SecondUnit,
            await workspaceEntityRepository.GetByIdAsync(request.Dto.WorkspaceId), request.UserId);

        await relationRepository.AddAsync(relation);
        
        await relationRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<RelationDto>(relation);
    }
}