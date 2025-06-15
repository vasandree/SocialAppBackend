using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using User.Contracts.Commands;
using User.Contracts.Dtos;
using User.Contracts.Queries;

namespace User.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("users/settings")]
public class UserSettingsController : ControllerBase
{
    private readonly ISender _mediator;

    public UserSettingsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetUserSettingsQuery(User.GetUserId())));
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] UserSettingsDto userSettingsDto)
    {
        return Ok(await _mediator.Send(new EditUserSettingsCommand(User.GetUserId(), userSettingsDto)));
    }
}