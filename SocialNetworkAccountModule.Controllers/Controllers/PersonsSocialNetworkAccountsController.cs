using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using SocialNetworkAccountModule.UseCases.Interfaces.Commands.PersonSocialNetworkAccount;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Requests;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;
using SocialNetworkAccountModule.UseCases.Interfaces.Queries;

namespace SocialNetworkAccountModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("persons")]
public sealed class PersonsSocialNetworkAccountsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [Route("{id:guid}/social_networks")]
    [ProducesResponseType(typeof(List<SocialNetworkAccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPersonsSocialNetworkAccounts(Guid id)
        => Ok(await mediator.Send(new GetPersonsSocialNetworkAccountsQuery(User.GetUserId(), id)));
    

    [HttpPost]
    [Route("{id:guid}/social_networks")]
    public async Task<IActionResult> AddSocialNetworkAccount(Guid id,
        [FromBody] AddSocialNetworkAccountDto addSocialNetworkAccountDto)
        => Ok(await mediator.Send(
            new AddSocialNetworkAccountCommand(User.GetUserId(), id, addSocialNetworkAccountDto)));
    

    [HttpPut]
    [Route("social_networks/{id:guid}")]
    public async Task<IActionResult> EditSocialNetworkAccount(Guid id,
        [FromBody] EditSocialNetworkAccountDto editSocialNetworkAccountDto)
    
        => Ok(await mediator.Send(
            new EditSocialNetworkAccountCommand(User.GetUserId(), id, editSocialNetworkAccountDto)));

    [HttpDelete]
    [Route("social_networks/{id:guid}")]
    public async Task<IActionResult> DeleteSocialNetworkAccount(Guid id)
        => Ok(await mediator.Send(new DeleteSocialNetworkAccountCommand(User.GetUserId(), id)));
    
}