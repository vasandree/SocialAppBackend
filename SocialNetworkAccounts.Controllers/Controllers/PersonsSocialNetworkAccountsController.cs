using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Dtos.Requests;
using SocialNetworkAccounts.Contracts.Queries;

namespace SocialNetworkAccounts.Controllers.Controllers;

[Authorize(Policy = "UserExists")]
[ApiController]
[Route("persons")]
public class PersonsSocialNetworkAccountsController : ControllerBase
{
    private readonly ISender _mediator;

    public PersonsSocialNetworkAccountsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:guid}/social_networks")]
    public async Task<IActionResult> GetPersonsSocialNetworkAccounts(Guid id)
    {
        return Ok(await _mediator.Send(new GetPersonsSocialNetworkAccountsQuery(User.GetUserId()!.Value, id)));
    }

    [HttpPost]
    [Route("{id:guid}/social_networks")]
    public async Task<IActionResult> AddSocialNetworkAccount(Guid id,
        [FromBody] AddSocialNetworkAccountDto addSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new AddSocialNetworkAccountCommand(User.GetUserId()!.Value, id, addSocialNetworkAccountDto)));
    }

    [HttpPut]
    [Route("{id:guid}/social_networks")]
    public async Task<IActionResult> EditSocialNetworkAccount(Guid id,
        [FromBody] EditSocialNetworkAccountDto editSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new EditSocialNetworkAccountCommand(User.GetUserId()!.Value, id, editSocialNetworkAccountDto)));
    }

    [HttpDelete]
    [Route("{id:guid}/social_networks")]
    public async Task<IActionResult> DeleteSocialNetworkAccount(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteSocialNetworkAccountCommand(User.GetUserId()!.Value, id)));
    }
}