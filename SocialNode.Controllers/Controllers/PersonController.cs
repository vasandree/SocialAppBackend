using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using SocialNode.Contracts.Commands.Person;
using SocialNode.Contracts.Dtos.Requests;
using SocialNode.Contracts.Queries;

namespace SocialNode.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("persons")]
public class PersonController : ControllerBase
{
    private readonly ISender _mediator;

    public PersonController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPersons([FromQuery] string? searchTerm = null, [FromQuery] int page = 1)
    {
        return Ok(await _mediator.Send(new GetPersonsQuery(User.GetUserId(), page, searchTerm)));
    }

    [HttpGet("{personId:guid}")]
    public async Task<IActionResult> GetPerson(Guid personId)
    {
        return Ok(await _mediator.Send(new GetPersonQuery(personId, User.GetUserId())));
    }

    [HttpPut]
    [Route("{personId:guid}")]
    public async Task<IActionResult> UpdatePerson(Guid personId, [FromForm] PersonRequestDto personDto)
    {
        return Ok(await _mediator.Send(new EditPersonCommand(User.GetUserId(), personId, personDto)));
    }

    [HttpDelete]
    [Route("{personId:guid}")]
    public async Task<IActionResult> DeletePerson(Guid personId)
    {
        return Ok(await _mediator.Send(new DeletePersonCommand(User.GetUserId(), personId)));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromForm] PersonRequestDto createPersonDto)
    {
        return Ok(await _mediator.Send(new CreatePersonCommand(User.GetUserId(), createPersonDto)));
    }
}