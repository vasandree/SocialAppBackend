using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using Workspace.Contracts.Commands.Relation;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Repositories;
using Workspace.Domain.Entities;

namespace Workspace.Application.Features.Commands.Relation;

public class CreateRelationCommandHandler : IRequestHandler<CreateRelationCommand, RelationDto>
{
    private readonly IMapper _mapper;
    private readonly IRelationRepository _relationRepository;
    private readonly IBaseSocialNodeRepository<BaseSocialNode> _baseSocialNodeRepository;
    private readonly IWorkspaceEntityRepository _workspaceEntityRepository;

    public CreateRelationCommandHandler(IMapper mapper, IRelationRepository relationRepository,
        IBaseSocialNodeRepository<BaseSocialNode> baseSocialNodeRepository,
        IWorkspaceEntityRepository workspaceEntityRepository)
    {
        _mapper = mapper;
        _relationRepository = relationRepository;
        _baseSocialNodeRepository = baseSocialNodeRepository;
        _workspaceEntityRepository = workspaceEntityRepository;
    }

    public async Task<RelationDto> Handle(CreateRelationCommand request, CancellationToken cancellationToken)
    {
        if (!await _baseSocialNodeRepository.CheckIfExists(request.Dto.FirstUnit) ||
            !await _baseSocialNodeRepository.CheckIfExists(request.Dto.SecondUnit))
            throw new BadRequest("Invalid social node IDs provided for relation creation.");

        if (!await _baseSocialNodeRepository.CheckIfUserIsCreator(request.UserId, request.Dto.FirstUnit) ||
            !await _baseSocialNodeRepository.CheckIfUserIsCreator(request.UserId, request.Dto.SecondUnit))
            throw new Forbidden("You are not the creator of one or both social nodes involved in this relation.");

        var relation = new RelationEntity
        {
            Name = request.Dto.Name,
            Description = request.Dto.Description,
            Color = request.Dto.Color,
            FirstSocialNode = request.Dto.FirstUnit,
            SecondSocialNode = request.Dto.SecondUnit,
            WorkspaceId = request.Dto.WorkspaceId,
            WorkspaceEntity = await _workspaceEntityRepository.GetByIdAsync(request.Dto.WorkspaceId),
            CreatorId = request.UserId
        };

        await _relationRepository.AddAsync(relation);

        return _mapper.Map<RelationDto>(relation);
    }
}