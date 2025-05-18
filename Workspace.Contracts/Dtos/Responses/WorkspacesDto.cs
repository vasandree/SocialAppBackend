using Shared.Contracts.Dtos;

namespace Workspace.Contracts.Dtos.Responses;

public class WorkspacesDto
{
    public required IReadOnlyList<ListedWorkspaceDto> Workspaces { get; set; }
    public required Pagination Pagination { get; set; }
}