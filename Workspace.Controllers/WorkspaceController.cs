using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using Workspace.Contracts.Commands;
using Workspace.Contracts.Dtos.Requests;
using Workspace.Contracts.Queries;

namespace Workspace.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("workspaces")]
public class WorkspaceController : ControllerBase
{
    private readonly ISender _sender;

    public WorkspaceController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkspaces([FromQuery] int page = 1)
    {
        return Ok(await _sender.Send(new GetWorkspacesQuery(User.GetUserId(), page)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkspace(Guid id)
    {
        return Ok(await _sender.Send(new GetWorkspaceQuery(User.GetUserId(), id)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkspace([FromBody] ShortenWorkspaceDto dto)
    {
        var id = await _sender.Send(new CreateWorkspaceCommand(User.GetUserId(), dto));
        return Ok(new { WorkspaceId = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkspace(Guid id, [FromBody] ShortenWorkspaceDto dto)
    {
        return Ok(await _sender.Send(new EditWorkspaceCommand(User.GetUserId(), id, dto)));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkspace(Guid id)
    {
        return Ok(await _sender.Send(new DeleteWorkspaceCommand(User.GetUserId(), id)));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateWorkspace(Guid id, [FromBody] string content)
    {
        return Ok(await _sender.Send(new EditWorkspaceContentCommand(User.GetUserId(), id, content)));
    }
}