using MediatR;
using Workspace.Contracts.Dtos.Requests;
using Workspace.Contracts.Dtos.Responses;

namespace Workspace.Contracts.Commands.Relation;

public record UpdateRelationCommand(Guid UserId, Guid Id, EditRelationDto Dto) : IRequest<RelationDto>;