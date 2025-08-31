using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using SocialNodeModule.UseCases.Interfaces.Commands.ClusterOfPeople;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("clusters")]
public sealed class ClusterController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedClusterDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClusters([FromQuery] string? searchTerm, [FromQuery] int page = 1)
        => Ok(await mediator.Send(new GetClustersQuery(User.GetUserId(), page, searchTerm)));
    

    [HttpGet]
    [Route("{clusterId:guid}")]
    [ProducesResponseType(typeof(ClusterOfPeopleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCluster(Guid clusterId)
        => Ok(await mediator.Send(new GetClusterQuery(clusterId, User.GetUserId())));
    

    [HttpPost]
    public async Task<IActionResult> CreateCluster([FromForm] ClusterRequestDto clusterDto)
    {
        return Ok(await mediator.Send(new CreateClusterCommand(User.GetUserId(), clusterDto)));
    }

    [HttpPut]
    [Route("{clusterId:guid}")]
    public async Task<IActionResult> UpdateCluster(Guid clusterId, [FromForm] ClusterRequestDto clusterDto)
        => Ok(await mediator.Send(new EditClusterCommand(User.GetUserId(), clusterId, clusterDto)));
    

    [HttpDelete]
    [Route("{clusterId:guid}")]
    public async Task<IActionResult> DeleteCluster(Guid clusterId)
        => Ok(await mediator.Send(new DeleteClusterCommand(User.GetUserId(), clusterId)));
    
}