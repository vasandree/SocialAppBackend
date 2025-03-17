using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos.Requests;
using UserService.Application.Dtos.Responses;
using UserService.Application.Features.Commands.Login;
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