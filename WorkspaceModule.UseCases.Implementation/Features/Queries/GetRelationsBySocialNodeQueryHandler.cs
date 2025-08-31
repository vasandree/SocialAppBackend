using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.Domain.Entities;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.UseCases.Interfaces.Queries;

namespace WorkspaceModule.UseCases.Implementation.Features.Queries;

internal sealed class GetRelationsBySocialNodeQueryHandler(
    IMapper mapper,
    IRelationRepository relationRepository,
    IBaseSocialNodeRepository<BaseSocialNode> baseSocialNodeRepository)
    : IRequestHandler<GetRelationsBySocialNodeQuery, List<RelationDto>>
{
    public async Task<List<RelationDto>> Handle(GetRelationsBySocialNodeQuery request,
        CancellationToken cancellationToken)
    {
        if (!await baseSocialNodeRepository.CheckIfExists(request.SocialNodeId))
            throw new NotFound("Social node not found");

        if (!await baseSocialNodeRepository.CheckIfUserIsCreator(request.UserId, request.SocialNodeId))
            throw new Forbidden("You are not the creator of this social node");

        var relations = await relationRepository.FindAsync(x =>
            x.FirstSocialNode == request.SocialNodeId || x.SecondSocialNode == request.SocialNodeId);

        return mapper.Map<List<RelationDto>>(relations);
    }
}