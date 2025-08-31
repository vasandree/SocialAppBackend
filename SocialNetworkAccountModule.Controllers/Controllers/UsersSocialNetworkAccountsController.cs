using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using Shared.Domain.Exceptions;
using SocialNetworkAccountModule.UseCases.Interfaces.Commands.UserSocialNetworkAccount;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Requests;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;
using SocialNetworkAccountModule.UseCases.Interfaces.Queries;

namespace SocialNetworkAccountModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("users")]
public sealed class UsersSocialNetworkAccountsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [Route("{id:guid}/social_networks")]
    [ProducesResponseType(typeof(List<SocialNetworkAccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPersonsSocialNetworkAccounts(Guid id)
        => Ok(await sender.Send(new GetUsersSocialNetworkAccountsQuery(id)));
    

    [HttpPost]
    [Route("{id:guid}/social_networks")]
    public async Task<IActionResult> AddSocialNetworkAccount(Guid id,
        [FromBody] AddSocialNetworkAccountDto addSocialNetworkAccountDto)
    {
        var userId = User.GetUserId();

        if (userId != id)
            throw new Forbidden("This is not the same user id.");

        return Ok(await sender.Send(
            new AddSocialNetworkAccountCommand(User.GetUserId(), addSocialNetworkAccountDto)));
    }

    [HttpPut]
    [Route("social_networks/{id:guid}")]
    public async Task<IActionResult> EditSocialNetworkAccount(Guid id,
        [FromBody] EditSocialNetworkAccountDto editSocialNetworkAccountDto)
        => Ok(await sender.Send(
            new EditSocialNetworkAccountCommand(User.GetUserId(), id, editSocialNetworkAccountDto)));
    

    [HttpDelete]
    [Route("social_networks/{id:guid}")]
    public async Task<IActionResult> DeleteSocialNetworkAccount(Guid id)
        => Ok(await sender.Send(new DeleteSocialNetworkAccountCommand(User.GetUserId(), id)));
    
}