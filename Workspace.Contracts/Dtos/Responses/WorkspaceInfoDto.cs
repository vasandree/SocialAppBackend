using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Workspace.Contracts.Dtos.Responses;

public class WorkspaceInfoDto
{
    public Guid WorkspaceId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public JsonObject Content { get; set; }
}