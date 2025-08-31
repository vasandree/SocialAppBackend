using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

public record WorkspaceInfoDto(Guid WorkspaceId, string Name, string? Description, JsonObject Content);