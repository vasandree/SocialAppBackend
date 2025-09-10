namespace WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

public record ListedWorkspaceDto
{
    public Guid WorkspaceId { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
};