using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNode.Contracts.Commands.Person;
using SocialNode.Contracts.Dtos.Requests;
using SocialNode.Contracts.Queries;

namespace SocialNode.Controllers.Controllers;

[Authorize]
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
        return Ok(await _mediator.Send(new GetPersonsQuery(User.GetUserId()!.Value, page, searchTerm)));
    }

    [HttpGet("{personId:guid}")]
    public async Task<IActionResult> GetPerson(Guid personId)
    {
        return Ok(await _mediator.Send(new GetPersonQuery(personId, User.GetUserId()!.Value)));
    }

    [HttpPut]
    [Route("{personId:guid}")]
    public async Task<IActionResult> UpdatePerson(Guid personId, [FromBody] PersonRequestDto personDto)
    {
        return Ok(await _mediator.Send(new EditPersonCommand(User.GetUserId()!.Value, personId, personDto)));
    }

    [HttpDelete]
    [Route("{personId:guid}")]
    public async Task<IActionResult> DeletePerson(Guid personId)
    {
        return Ok(await _mediator.Send(new DeletePersonCommand(User.GetUserId()!.Value, personId)));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] PersonRequestDto createPersonDto)
    {
        return Ok(await _mediator.Send(new CreatePersonCommand(User.GetUserId()!.Value, createPersonDto)));
    }
}