using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Event.Controllers.Controllers;

[ApiController]
[Route("events")]
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
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent()
    {
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        return Ok();
    }
}