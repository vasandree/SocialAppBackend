using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkAccounts.Application.Dtos.Requests;
using SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.AddSocialNetworkAccount;
using SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.DeleteSocialNetworkAccount;
using SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.EditSocialNetworkAccount;
using SocialNetworkAccounts.Application.Features.Queries.GetPersonsSocialNetworkAccounts;

namespace SocialNetworkAccounts.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("social_network_service/persons")]
public class PersonsSocialNetworkAccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsSocialNetworkAccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetPersonsSocialNetworkAccounts(Guid id)
    {
        return Ok(await _mediator.Send(new GetPersonsSocialNetworkAccountsCommand(User.GetUserId()!.Value, id)));
    }

    [HttpPost]
    [Route("{id:guid}")]
    public async Task<IActionResult> AddSocialNetworkAccount(Guid id,
        [FromBody] AddSocialNetworkAccountDto addSocialNetworkAccountDto)
    {
        return Ok(await _mediator.Send(
            new AddSocialNetworkAccountCommand(User.GetUserId()!.Value, id, addSocialNetworkAccountDto)));
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