using MediatR;
using Workspace.Contracts.Dtos.Requests;
using Workspace.Contracts.Dtos.Responses;

namespace Workspace.Contracts.Commands.Relation;

public record CreateRelationCommand(Guid UserId, CreateRelationDto Dto) : IRequest<RelationDto>;