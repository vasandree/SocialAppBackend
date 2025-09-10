using System.Text.Json.Nodes;

namespace WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

public record WorkspaceInfoDto
{
    public Guid WorkspaceId { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public JsonObject Content { get; init; }
};