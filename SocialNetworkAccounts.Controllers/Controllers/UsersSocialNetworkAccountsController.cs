using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Configurations.Extensions;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Dtos.Requests;
using SocialNetworkAccounts.Contracts.Queries;

namespace SocialNetworkAccounts.Controllers.Controllers;

[Authorize]
[ApiController]
[Microsoft.AspNetCore.Components.Route("users")]
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
    public async Task<IActionResult> AddSocialNetworkAccount(
        [FromBody] AddSocialNetworkAccountDto addSocialNetworkAccountDto)
    {
        //todo: check if id belongs tp user

        return Ok(await _mediator.Send(
            new AddSocialNetworkAccountCommand(User.GetUserId()!.Value, addSocialNetworkAccountDto)));
    }

    [HttpPut]
    [Route("social_networks/{id:guid}")]
    public async Task<IActionResult> EditSocialNetworkAccount(Guid id,
        [FromBody] EditSocialNetworkAccountDto editSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new EditSocialNetworkAccountCommand(User.GetUserId()!.Value, id, editSocialNetworkAccountDto)));
    }

    [HttpDelete]
    [Route("social_networks/{id:guid}")]
    public async Task<IActionResult> DeleteSocialNetworkAccount(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteSocialNetworkAccountCommand(User.GetUserId()!.Value, id)));
    }
}