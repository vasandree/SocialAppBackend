using Event.Contracts.Commands.Event;
using Event.Contracts.Dtos.Requests;
using Event.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;

namespace Event.Controllers.Controllers;

[ApiController]
[Route("events")]
[Authorize]
[UserExists]
public class EventsController : ControllerBase
{
    private readonly ISender _sender;

    public EventsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        return Ok(await _sender.Send(new GetEventsQuery(User.GetUserId())));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        return Ok(await _sender.Send(new GetEventQuery(User.GetUserId(), id)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
    {
        return Ok(await _sender.Send(new CreateEventCommand(User.GetUserId(), eventDto)));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventDto eventDto)
    {
        return Ok(await _sender.Send(new UpdateEventCommand(User.GetUserId(), id, eventDto)));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        return Ok(await _sender.Send(new DeleteEventCommand(User.GetUserId(), id)));
    }
}