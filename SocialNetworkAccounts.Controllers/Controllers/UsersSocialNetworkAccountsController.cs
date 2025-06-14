using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Dtos.Requests;
using SocialNetworkAccounts.Contracts.Queries;

namespace SocialNetworkAccounts.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("users")]
public class UsersSocialNetworkAccountsController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersSocialNetworkAccountsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:guid}/social_networks")]
    public async Task<IActionResult> GetPersonsSocialNetworkAccounts(Guid id)
    {
        return Ok(await _mediator.Send(new GetUsersSocialNetworkAccountsQuery(id)));
    }

    [HttpPost]
    [Route("{id:guid}/social_networks")]
    public async Task<IActionResult> AddSocialNetworkAccount(Guid id,
        [FromBody] AddSocialNetworkAccountDto addSocialNetworkAccountDto)
    {
        var userId = User.GetUserId();

        if (userId != id)
            throw new Forbidden("This is not the same user id.");

        return Ok(await _mediator.Send(
            new AddSocialNetworkAccountCommand(User.GetUserId(), addSocialNetworkAccountDto)));
    }

    [HttpPut]
    [Route("social_networks/{id:guid}")]
    public async Task<IActionResult> EditSocialNetworkAccount(Guid id,
        [FromBody] EditSocialNetworkAccountDto editSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new EditSocialNetworkAccountCommand(User.GetUserId(), id, editSocialNetworkAccountDto)));
    }

    [HttpDelete]
    [Route("social_networks/{id:guid}")]
    public async Task<IActionResult> DeleteSocialNetworkAccount(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteSocialNetworkAccountCommand(User.GetUserId(), id)));
    }
}