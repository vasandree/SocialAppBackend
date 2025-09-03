using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using UserModule.UseCases.Interfaces.Commands;
using UserModule.UseCases.Interfaces.Queries;

namespace UserModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("users/settings")]
public sealed class UserSettingsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(UserSettingsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
        => Ok(await mediator.Send(new GetUserSettingsQuery(User.GetUserId())));
    

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] UserSettingsDto userSettingsDto)
        => Ok(await mediator.Send(new EditUserSettingsCommand(User.GetUserId(), userSettingsDto)));
    
}