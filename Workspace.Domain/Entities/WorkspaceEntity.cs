using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;
using Shared.Domain;

namespace Workspace.Domain.Entities;

public class WorkspaceEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public string ContentJson { get; set; } = "{}";

    [NotMapped]
    public JsonObject Content
    {
        get => JsonSerializer.Deserialize<JsonObject>(ContentJson) ?? new JsonObject();
        set => ContentJson = JsonSerializer.Serialize(value);
    }
    
    [Required]
    public Guid CreatorId { get; set; }
    
    public ICollection<Guid> SocialNodes { get; set; } = [];
    public ICollection<RelationEntity> Relations { get; set; } = [];
    public ICollection<Guid> Tasks { get; set; } = [];
    public ICollection<Guid> Events { get; set; } = [];
}