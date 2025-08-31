using MediatR;
using WorkspaceModule.UseCases.Interfaces.Dtos.Requests;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Interfaces.Commands.Relation;

public record CreateRelationCommand(Guid UserId, CreateRelationDto Dto) : IRequest<RelationDto>;