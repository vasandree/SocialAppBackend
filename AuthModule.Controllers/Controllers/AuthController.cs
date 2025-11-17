using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.UseCases.Interfaces.Dtos.Requests;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;

namespace AuthModule.Controllers.Controllers;

[ApiController]
[Route("auth")]
public sealed class GovnohController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] InitDataDto initData, [FromQuery] SocialNetwork socialNetwork)
        => Ok(await sender.Send(new LoginCommand(socialNetwork, initData)));


    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokensDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Refresh([FromBody] TokensDto tokensDto)
        => Ok(await sender.Send(new RefreshTokensCommand(tokensDto)));

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        => Ok(await sender.Send(new RegisterCommand(registerDto)));
}