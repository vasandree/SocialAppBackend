using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos.Requests;
using UserService.Application.Features.Commands.Login;
using UserService.Application.Features.Commands.LoginCommand;
using UserService.Application.Features.Commands.RefreshTokens;
using UserService.Domain.Enums;

namespace UserService.Presentation.Controllers;

[ApiController]
[Route("social_app/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] InitDataDto initData, [FromHeader] SocialNetwork socialNetwork)
    {
        return Ok(await _mediator.Send(new LoginCommand(socialNetwork, initData)));
    }

    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        return Ok(await _mediator.Send(new RefreshTokensCommand(User.GetUserId()!.Value, refreshToken)));
    }
}