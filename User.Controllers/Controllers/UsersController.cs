using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using User.Contracts.Commands;
using User.Contracts.Dtos.Requests;
using User.Contracts.Queries;

namespace User.Controllers.Controllers;

[Authorize]
[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? searchTerm = null)
    {
        return Ok(await _mediator.Send(new GetUsersQuery(User.GetUserId()!.Value, searchTerm)));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        return Ok(await _mediator.Send(new GetUserQuery(id)));
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, InitDataDto initData,
        SocialNetwork socialNetwork = SocialNetwork.Telegram)
    {
        return Ok(await _mediator.Send(new UpdateUserCommand(id, socialNetwork, initData)));
    }

    [HttpGet]
    [Route("profile")]
    public async Task<IActionResult> GetProfile()
    {
        return Ok(await _mediator.Send(new GetUserQuery(User.GetUserId()!.Value)));
    }
}