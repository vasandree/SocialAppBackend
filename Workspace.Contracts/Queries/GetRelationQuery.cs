using MediatR;
using Workspace.Contracts.Dtos.Responses;

namespace Workspace.Contracts.Queries;

public record GetRelationQuery(Guid UserId, Guid Id) : IRequest<RelationDto>;