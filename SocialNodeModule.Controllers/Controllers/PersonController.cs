using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using SocialNodeModule.UseCases.Interfaces.Commands.Person;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("persons")]
public sealed class PersonController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedPersonsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPersons([FromQuery] string? searchTerm, [FromQuery] int page = 1)
        => Ok(await mediator.Send(new GetPersonsQuery(User.GetUserId(), page, searchTerm)));


    [HttpGet("{personId:guid}")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPerson(Guid personId)
        => Ok(await mediator.Send(new GetPersonQuery(personId, User.GetUserId())));


    [HttpPut]
    [Route("{personId:guid}")]
    public async Task<IActionResult> UpdatePerson(Guid personId, [FromForm] PersonRequestDto personDto)
        => Ok(await mediator.Send(new EditPersonCommand(User.GetUserId(), personId, personDto)));


    [HttpDelete]
    [Route("{personId:guid}")]
    public async Task<IActionResult> DeletePerson(Guid personId)
        => Ok(await mediator.Send(new DeletePersonCommand(User.GetUserId(), personId)));


    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromForm] PersonRequestDto createPersonDto)
        => Ok(await mediator.Send(new CreatePersonCommand(User.GetUserId(), createPersonDto)));
}