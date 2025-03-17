using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace WorkspaceService.Domain.Entities;

public class Workspace
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public JsonObject Content { get; set; } = new JsonObject();
    
    public Guid CreatorId { get; set; }
    
    
}