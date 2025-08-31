using MediatR;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Queries;

public record GetRelationQuery(Guid UserId, Guid Id) : IRequest<RelationDto>;