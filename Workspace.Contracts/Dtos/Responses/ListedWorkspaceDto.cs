namespace Workspace.Contracts.Dtos.Responses;

public record ListedWorkspaceDto
{
    public string WorkspaceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}