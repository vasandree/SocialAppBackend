using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.UseCases.Interfaces.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using UserModule.UseCases.Interfaces.Dtos.Requests;

namespace AuthModule.Controllers.Controllers;

[ApiController]
[Route("auth")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] InitDataDto initData, [FromQuery] SocialNetwork socialNetwork) 
        => Ok(await sender.Send(new LoginCommand(socialNetwork, initData)));
    

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokensDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Refresh([FromBody] TokensDto tokensDto)
        => Ok(await sender.Send(new RefreshTokensCommand(tokensDto)));
    
}