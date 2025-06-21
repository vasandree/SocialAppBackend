using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using Workspace.Contracts.Commands.Relation;
using Workspace.Contracts.Dtos.Requests;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Queries;
using Workspace.Domain.Enums;

namespace Workspace.Controllers.Controllers;

[UserExists]
[Authorize]
[ApiController]
[Route("relations")]
public class RelationController : ControllerBase
{
    private readonly ISender _sender;

    public RelationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet, Route("{id:guid}")]
    [ProducesResponseType(typeof(RelationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRelation(Guid id)
    {
        return Ok(await _sender.Send(new GetRelationQuery(User.GetUserId(), id)));
    }

    [HttpGet, Route("/social_node/{id:guid}")]
    [ProducesResponseType(typeof(RelationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRelationBySocialNode(Guid id, [FromQuery] SocialNodeType type)
    {
        return Ok(await _sender.Send(new GetRelationsBySocialNodeQuery(User.GetUserId(), id, type)));
    }

    [HttpPost]
    [ProducesResponseType(typeof(RelationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRelation([FromBody] CreateRelationDto dto)
    {
        return Ok(await _sender.Send(new CreateRelationCommand(User.GetUserId(), dto)));
    }

    [HttpPut, Route("{id:guid}")]
    [ProducesResponseType(typeof(RelationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRelation(Guid id, [FromBody] EditRelationDto dto)
    {
        return Ok(await _sender.Send(new UpdateRelationCommand(User.GetUserId(), id, dto)));
    }

    [HttpDelete, Route("{id:guid}")]
    public async Task<IActionResult> DeleteRelation(Guid id)
    {
        return Ok(await _sender.Send(new DeleteRelationCommand(User.GetUserId(), id)));
    }
}