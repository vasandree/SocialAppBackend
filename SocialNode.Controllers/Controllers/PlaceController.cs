using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using SocialNode.Contracts.Commands.Place;
using SocialNode.Contracts.Dtos.Requests;
using SocialNode.Contracts.Queries;

namespace SocialNode.Controllers.Controllers;

[Authorize(Policy = "UserExists")]
[ApiController]
[Route("places")]
public class PlaceController : ControllerBase
{
    private readonly ISender _mediator;

    public PlaceController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlaces([FromQuery] string? searchTerm = null, [FromQuery] int page = 1)
    {
        return Ok(await _mediator.Send(new GetPlacesQuery(User.GetUserId(), page, searchTerm)));
    }

    [HttpGet]
    [Route("{placeId:guid}")]
    public async Task<IActionResult> GetPlace(Guid placeId)
    {
        return Ok(await _mediator.Send(new GetPlaceQuery(placeId, User.GetUserId())));
    }

    [HttpPut]
    [Route("{placeId:guid}")]
    public async Task<IActionResult> UpdatePlace(Guid placeId, [FromBody] PlaceRequestDto placeDto)
    {
        return Ok(await _mediator.Send(new EditPlaceCommand(placeId, User.GetUserId(), placeDto)));
    }

    [HttpDelete]
    [Route("{placeId:guid}")]
    public async Task<IActionResult> DeletePlace(Guid placeId)
    {
        return Ok(await _mediator.Send(new DeletePlaceCommand(User.GetUserId(), placeId)));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlace([FromBody] PlaceRequestDto createPlaceDto)
    {
        return Ok(await _mediator.Send(new CreatePlaceCommand(User.GetUserId(), createPlaceDto)));
    }
}