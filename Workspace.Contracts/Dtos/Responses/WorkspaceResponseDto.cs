using System.Net.Http.Json;

namespace Workspace.Contracts.Dtos.Responses;

public record WorkspaceResponseDto
{
    public Guid WorkspaceId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public JsonContent Content { get; set; }
}