using Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonService.Application.Dtos.Requests;
using PersonService.Application.Features.Commands.ClustersOfPeople.CreateClusterOfPeople;
using PersonService.Application.Features.Commands.ClustersOfPeople.DeleteClusterOfPeople;
using PersonService.Application.Features.Commands.ClustersOfPeople.EditClusterOfPeople;
using PersonService.Application.Features.Queries.GetClusterQuery;
using PersonService.Application.Features.Queries.GetClustersQuery;

namespace PersonService.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("person_service/clusters")]
public class ClusterController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClusterController(IMediator mediator)
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