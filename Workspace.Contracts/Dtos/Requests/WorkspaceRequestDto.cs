namespace Workspace.Contracts.Dtos.Requests;

public record ShortenWorkspaceDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}