using MediatR;
using WorkspaceModule.UseCases.Interfaces.Dtos.Requests;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Commands.Relation;

public record UpdateRelationCommand(Guid UserId, Guid Id, EditRelationDto Dto) : IRequest<RelationDto>;