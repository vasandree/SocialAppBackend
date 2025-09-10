namespace WorkspaceModule.UseCases.Interfaces.Dtos.Requests;

public record ShortenWorkspaceDto
{
    public Guid WorkspaceId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
};