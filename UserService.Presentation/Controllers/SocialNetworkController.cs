using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos.Requests;
using UserService.Application.Features.Commands.SocialNetwork.AddSocialNetworkAccount;
using UserService.Application.Features.Commands.SocialNetwork.DeleteSocialNetwork;
using UserService.Application.Features.Commands.SocialNetwork.EditSocialNetwork;
using UserService.Application.Features.Queries.GetSocialNetworkAccounts;

namespace UserService.Presentation.Controllers;

[Authorize]
[Route("user_service/social_network")]
public class SocialNetworkController : ControllerBase
{
    private IMediator _mediator;

    public SocialNetworkController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSocialNetworkAccounts()
    {
        return Ok(await _mediator.Send(new GetSocialNetworkAccountsCommand(User.GetUserId()!.Value)));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddSocialNetworkAccount([FromBody] AddSocialNetworkAccountDto addSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new AddSocialNetworkAccountCommand(User.GetUserId()!.Value, addSocialNetworkAccountDto)));
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> EditSocialNetworkAccount(Guid id,
        [FromBody] EditSocialNetworkAccountDto addSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new EditSocialNetworkAccountCommand(User.GetUserId()!.Value, id, addSocialNetworkAccountDto)));
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteSocialNetworkAccount(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteSocialNetworkAccountCommand(User.GetUserId()!.Value, id)));
    }
}