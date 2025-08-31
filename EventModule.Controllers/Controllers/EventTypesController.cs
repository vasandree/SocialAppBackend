using EventModule.UseCases.Interfaces.Commands.EventType;
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
[Route("event_types")]
[Authorize]
[UserExists]
public sealed class EventTypesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<EventTypeResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEventTypes([FromQuery] string? name = null)
        => Ok(await sender.Send(new GetEventTypesQuery(User.GetUserId(), name)));

    [HttpPost]
    public async Task<IActionResult> CreateEventType([FromBody] EventTypeRequestDto eventTypeRequestDto)
        => Ok(await sender.Send(new CreateEventTypeCommand(User.GetUserId(), eventTypeRequestDto)));
    

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateEventType(Guid id, [FromBody] EventTypeRequestDto eventTypeRequestDto)
        => Ok(await sender.Send(new EditEventTypeCommand(User.GetUserId(), id, eventTypeRequestDto)));
    

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteEventType(Guid id)
        => Ok(await sender.Send(new DeleteEventTypeCommand(User.GetUserId(), id)));
    
}