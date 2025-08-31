using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using WorkspaceModule.Domain.Enums;
using WorkspaceModule.UseCases.Interfaces.Commands.Relation;
using WorkspaceModule.UseCases.Interfaces.Dtos.Requests;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.UseCases.Interfaces.Queries;

namespace WorkspaceModule.Controllers.Controllers;

[UserExists]
[Authorize]
[ApiController]
[Route("relations")]
public sealed class RelationController(ISender sender) : ControllerBase
{
    [HttpGet, Route("{id:guid}")]
    [ProducesResponseType(typeof(RelationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRelation(Guid id)
        => Ok(await sender.Send(new GetRelationQuery(User.GetUserId(), id)));
    

    [HttpGet, Route("social_node/{id:guid}/relations")]
    [ProducesResponseType(typeof(List<RelationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRelationBySocialNode(Guid id, [FromQuery] SocialNodeType type)
        => Ok(await sender.Send(new GetRelationsBySocialNodeQuery(User.GetUserId(), id, type)));
    

    [HttpPost]
    [ProducesResponseType(typeof(RelationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRelation([FromBody] CreateRelationDto dto)
    {
        return Ok(await sender.Send(new CreateRelationCommand(User.GetUserId(), dto)));
    }

    [HttpPut, Route("{id:guid}")]
    [ProducesResponseType(typeof(RelationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRelation(Guid id, [FromBody] EditRelationDto dto)
        => Ok(await sender.Send(new UpdateRelationCommand(User.GetUserId(), id, dto)));
    

    [HttpDelete, Route("{id:guid}")]
    public async Task<IActionResult> DeleteRelation(Guid id)
        => Ok(await sender.Send(new DeleteRelationCommand(User.GetUserId(), id)));
    
}