using EventModule.UseCases.Interfaces.Commands.Event;
using EventModule.UseCases.Interfaces.Dtos.Requests;
using EventModule.UseCases.Interfaces.Dtos.Responses;
using EventModule.UseCases.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;

namespace EventModule.Controllers.Controllers;

[ApiController]
[Route("events")]
[Authorize]
[UserExists]
public sealed class EventsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ListedEventDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEvents()
        => Ok(await sender.Send(new GetEventsQuery(User.GetUserId())));
    

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEvent(Guid id)
        => Ok(await sender.Send(new GetEventQuery(User.GetUserId(), id)));
    

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        => Ok(await sender.Send(new CreateEventCommand(User.GetUserId(), eventDto)));
    

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventDto eventDto)
        => Ok(await sender.Send(new UpdateEventCommand(User.GetUserId(), id, eventDto)));
    

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
        => Ok(await sender.Send(new DeleteEventCommand(User.GetUserId(), id)));
    
}