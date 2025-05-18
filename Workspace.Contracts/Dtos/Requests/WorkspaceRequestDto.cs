namespace Workspace.Contracts.Dtos.Requests;

public record WorkspaceRequestDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}