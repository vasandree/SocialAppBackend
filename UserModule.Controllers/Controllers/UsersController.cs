using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using Shared.Domain;
using UserModule.UseCases.Interfaces.Commands;
using UserModule.UseCases.Interfaces.Dtos.Requests;
using UserModule.UseCases.Interfaces.Queries;

namespace UserModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("users")]
public sealed class UsersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? searchTerm = null)
        =>Ok(await sender.Send(new GetUsersQuery(User.GetUserId(), searchTerm)));
    

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
        => Ok(await sender.Send(new GetUserQuery(id)));
    

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, InitDataDto initData,
        SocialNetwork socialNetwork = SocialNetwork.Telegram)
        => Ok(await sender.Send(new UpdateUserCommand(id, socialNetwork, initData)));
    

    [HttpGet]
    [Route("profile")]
    public async Task<IActionResult> GetProfile()
        => Ok(await sender.Send(new GetUserQuery(User.GetUserId())));
    
}