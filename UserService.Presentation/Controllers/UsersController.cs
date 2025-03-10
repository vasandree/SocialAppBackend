using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos.Requests;
using UserService.Application.Features.Commands.UpdateUser;
using UserService.Application.Features.Queries.GetUser;
using UserService.Application.Features.Queries.GetUsers;
using UserService.Domain.Enums;

namespace UserService.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("social_app/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? searchTerm = null)
    {
        return Ok(await _mediator.Send(new GetUsersCommand(User.GetUserId()!.Value, searchTerm)));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        return Ok(await _mediator.Send(new GetUserCommand(id)));
    }
    
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, InitDataDto initData,
        SocialNetwork socialNetwork = SocialNetwork.Telegram)
    {
        return Ok(await _mediator.Send(new UpdateUserCommand(id, socialNetwork, initData)));
    }
}