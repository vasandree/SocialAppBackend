using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace Workspace.Domain.Entities;

public class Workspace
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    
    public JsonObject Content { get; set; } = new JsonObject();
    
    [Required]
    public Guid CreatorId { get; set; }
    
    public List<WorkspaceUser> WorkspaceUsers { get; set; } = new List<WorkspaceUser>();
    public List<WorkspacePerson> WorkspacePersons { get; set; } = new List<WorkspacePerson>();
}