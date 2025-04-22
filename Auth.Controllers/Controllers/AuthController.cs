using Auth.Contracts.Commands;
using Auth.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Contracts.Dtos.Requests;
using User.Domain.Enums;

namespace Auth.Controllers.Controllers;

[ApiController]
[Route("user_service/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] InitDataDto initData, [FromQuery] SocialNetwork socialNetwork)
    {
        return Ok(await _mediator.Send(new LoginCommand(socialNetwork, initData)));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokensDto tokensDto)
    {
        return Ok(await _mediator.Send(new RefreshTokensCommand(tokensDto)));
    }
}