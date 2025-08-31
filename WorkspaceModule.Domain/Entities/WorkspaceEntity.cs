using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;
using Shared.Domain;

namespace WorkspaceModule.Domain.Entities;

public class WorkspaceEntity : CreatableEntity
{
    private WorkspaceEntity() { }
    
    public WorkspaceEntity(string name, string description, Guid userId)
    {
        Name = name;
        Description = description;
        CreatorId = userId;
    }

    [Required]
    private string Name { get; set; }
    
    private string? Description { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public string ContentJson { get; set; } = "{}";

    [NotMapped]
    public JsonObject Content
    {
        get => JsonSerializer.Deserialize<JsonObject>(ContentJson) ?? new JsonObject();
        init => ContentJson = JsonSerializer.Serialize(value);
    }
    
    public ICollection<Guid> SocialNodes { get; set; } = [];
    public ICollection<RelationEntity> Relations { get; set; } = [];
    public ICollection<Guid> Tasks { get; set; } = [];
    public ICollection<Guid> Events { get; set; } = [];
    
    public bool CheckIfUserIsCreator(Guid userId) => CreatorId == userId;
    
    public void RemoveEvent(Guid eventId) => Events.Remove(eventId);

    public void UpdateInfo(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public void UpdateContent(string content) => ContentJson = content;
}