using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Queries;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Queries;

public class GetRelationsBySocialNodeQueryHandler : IRequestHandler<GetRelationsBySocialNodeQuery, List<RelationDto>>
{
    private readonly IMapper _mapper;
    private readonly IRelationRepository _relationRepository;
    private readonly IBaseSocialNodeRepository<BaseSocialNode> _baseSocialNodeRepository;

    public GetRelationsBySocialNodeQueryHandler(IMapper mapper, IRelationRepository relationRepository,
        IBaseSocialNodeRepository<BaseSocialNode> baseSocialNodeRepository)
    {
        _mapper = mapper;
        _relationRepository = relationRepository;
        _baseSocialNodeRepository = baseSocialNodeRepository;
    }


    public async Task<List<RelationDto>> Handle(GetRelationsBySocialNodeQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _baseSocialNodeRepository.CheckIfExists(request.SocialNodeId))
            throw new NotFound("Social node not found");

        if (!await _baseSocialNodeRepository.CheckIfUserIsCreator(request.UserId, request.SocialNodeId))
            throw new Forbidden("You are not the creator of this social node");

        var relations = await _relationRepository.FindAsync(x =>
            x.FirstSocialNode == request.SocialNodeId || x.SecondSocialNode == request.SocialNodeId);

        return _mapper.Map<List<RelationDto>>(relations);
    }
}