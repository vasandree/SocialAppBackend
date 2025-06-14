using Event.Contracts.Commands.EventType;
using Event.Contracts.Dtos.Requests;
using Event.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;

namespace Event.Controllers.Controllers;

[ApiController]
[Route("event_types")]
[Authorize]
[UserExists]
public class EventTypesController : ControllerBase
{
    private readonly ISender _sender;

    public EventTypesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetEventTypes([FromQuery] string? name = null)
    {
        return Ok(await _sender.Send(new GetEventTypesQuery(User.GetUserId(), name)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEventType([FromBody] EventTypeRequestDto eventTypeRequestDto)
    {
        return Ok(await _sender.Send(new CreateEventTypeCommand(User.GetUserId(), eventTypeRequestDto)));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateEventType(Guid id, [FromBody] EventTypeRequestDto eventTypeRequestDto)
    {
        return Ok(await _sender.Send(new EditEventTypeCommand(User.GetUserId(), id, eventTypeRequestDto)));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteEventType(Guid id)
    {
        return Ok(await _sender.Send(new DeleteEventTypeCommand(User.GetUserId(), id)));
    }
}