using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using WorkspaceModule.UseCases.Interfaces.Commands.Workspace;
using WorkspaceModule.UseCases.Interfaces.Dtos.Requests;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.UseCases.Interfaces.Queries;

namespace WorkspaceModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("workspaces")]
public sealed class WorkspaceController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(WorkspacesDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkspaces([FromQuery] int page = 1)
        => Ok(await sender.Send(new GetWorkspacesQuery(User.GetUserId(), page)));


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WorkspaceResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkspace(Guid id)
        => Ok(await sender.Send(new GetWorkspaceQuery(User.GetUserId(), id)));


    [HttpPost]
    public async Task<IActionResult> CreateWorkspace([FromBody] UpdateWorkspaceDto dto)
    {
        var id = await sender.Send(new CreateWorkspaceCommand(User.GetUserId(), dto));
        return Ok(new { WorkspaceId = id });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ListedWorkspaceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateWorkspace(Guid id, [FromBody] UpdateWorkspaceDto dto)
        => Ok(await sender.Send(new EditWorkspaceCommand(User.GetUserId(), id, dto)));


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkspace(Guid id)
        => Ok(await sender.Send(new DeleteWorkspaceCommand(User.GetUserId(), id)));


    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(WorkspaceResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateWorkspace(Guid id, [FromBody] string content)
        => Ok(await sender.Send(new EditWorkspaceContentCommand(User.GetUserId(), id, content)));
}