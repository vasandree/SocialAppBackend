using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using SocialNodeModule.UseCases.Interfaces.Commands.Place;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Place;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("places")]
public sealed class PlaceController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedPlacesDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlaces([FromQuery] string? searchTerm, [FromQuery] int page = 1)
        => Ok(await mediator.Send(new GetPlacesQuery(User.GetUserId(), page, searchTerm)));
    

    [HttpGet]
    [Route("{placeId:guid}")]
    [ProducesResponseType(typeof(PlaceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlace(Guid placeId)
        => Ok(await mediator.Send(new GetPlaceQuery(placeId, User.GetUserId())));
    

    [HttpPut]
    [Route("{placeId:guid}")]
    public async Task<IActionResult> UpdatePlace(Guid placeId, [FromForm] PlaceRequestDto placeDto)
        => Ok(await mediator.Send(new EditPlaceCommand(placeId, User.GetUserId(), placeDto))); 

    [HttpDelete]
    [Route("{placeId:guid}")]
    public async Task<IActionResult> DeletePlace(Guid placeId) 
        => Ok(await mediator.Send(new DeletePlaceCommand(User.GetUserId(), placeId)));
    

    [HttpPost]
    public async Task<IActionResult> CreatePlace([FromForm] PlaceRequestDto createPlaceDto)
        => Ok(await mediator.Send(new CreatePlaceCommand(User.GetUserId(), createPlaceDto)));
    
}