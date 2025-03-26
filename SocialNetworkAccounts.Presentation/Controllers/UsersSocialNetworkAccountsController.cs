using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkAccounts.Application.Dtos.Requests;
using SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.AddSocialNetworkAccount;
using SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.DeleteSocialNetworkAccount;
using SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.EditSocialNetworkAccount;
using SocialNetworkAccounts.Application.Features.Queries.GetUsersSocialNetworkAccounts;

namespace SocialNetworkAccounts.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("social_network_service/users")]
public class UsersSocialNetworkAccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersSocialNetworkAccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetPersonsSocialNetworkAccounts(Guid id)
    {
        return Ok(await _mediator.Send(new GetUsersSocialNetworkAccountsCommand(id)));
    }

    [HttpPost]
    public async Task<IActionResult> AddSocialNetworkAccount(
        [FromBody] AddSocialNetworkAccountDto addSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new AddSocialNetworkAccountCommand(User.GetUserId()!.Value, addSocialNetworkAccountDto)));
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> EditSocialNetworkAccount(Guid id,
        [FromBody] EditSocialNetworkAccountDto editSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new EditSocialNetworkAccountCommand(User.GetUserId()!.Value, id, editSocialNetworkAccountDto)));
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteSocialNetworkAccount(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteSocialNetworkAccountCommand(User.GetUserId()!.Value, id)));
    }
}