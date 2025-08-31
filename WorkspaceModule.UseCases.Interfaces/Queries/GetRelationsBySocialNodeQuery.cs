using MediatR;
using WorkspaceModule.Domain.Enums;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Queries;

public record GetRelationsBySocialNodeQuery(Guid UserId, Guid SocialNodeId, SocialNodeType SocialNodeType)
    : IRequest<List<RelationDto>>;