using MediatR;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Domain.Enums;

namespace Workspace.Contracts.Queries;

public record GetRelationsBySocialNodeQuery(Guid UserId, Guid SocialNodeId, SocialNodeType SocialNodeType)
    : IRequest<List<RelationDto>>;