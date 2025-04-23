using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNode.Contracts.Commands.ClusterOfPeople;
using SocialNode.Contracts.Dtos.Requests;
using SocialNode.Contracts.Queries;

namespace SocialNode.Controllers.Controllers;

[Authorize]
[ApiController]
[Route("clusters")]
public class ClusterController : ControllerBase
{
    private readonly ISender _mediator;

    public ClusterController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetClusters([FromQuery] string? searchTerm = null, [FromQuery] int page = 1)
    {
        return Ok(await _mediator.Send(new GetClustersQuery(User.GetUserId()!.Value, page, searchTerm)));
    }
    
    [HttpGet]
    [Route("{clusterId:guid}")]
    public async Task<IActionResult> GetCluster(Guid clusterId)
    {
        return Ok(await _mediator.Send(new GetClusterQuery(clusterId, User.GetUserId()!.Value)));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCluster([FromBody] ClusterRequestDto clusterDto)
    {
        return Ok(await _mediator.Send(new CreateClusterCommand(User.GetUserId()!.Value, clusterDto)));
    }
    
    [HttpPut]
    [Route("{clusterId:guid}")]
    public async Task<IActionResult> UpdateCluster(Guid clusterId, [FromBody] ClusterRequestDto clusterDto)
    {
        return Ok(await _mediator.Send(new EditClusterCommand(User.GetUserId()!.Value, clusterId, clusterDto)));
    }
    
    [HttpDelete]
    [Route("{clusterId:guid}")]
    public async Task<IActionResult> DeleteCluster(Guid clusterId)
    {
        return Ok(await _mediator.Send(new DeleteClusterCommand(User.GetUserId()!.Value, clusterId)));
    }
}