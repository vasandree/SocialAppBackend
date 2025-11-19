using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain;

namespace WorkspaceModule.Domain.Entities;

public class RelationEntity : CreatableEntity
{
    private RelationEntity() { }
    
    public RelationEntity(string name, string? description, string color, Guid firstUnit, Guid secondUnit, WorkspaceEntity workspaceEntity, Guid userId)
    {
        Name = name;
        Description = description;
        Color = color;
        FirstSocialNode = firstUnit;
        SecondSocialNode = secondUnit;
        WorkspaceEntity = workspaceEntity;
        WorkspaceId = workspaceEntity.Id;
        CreatorId = userId;
    }

    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Color { get; set; }
    
    [Required]
    public Guid FirstSocialNode { get; private set; }
    
    [Required]
    public Guid SecondSocialNode { get; private set; }
    
    [Required]
    [ForeignKey("Workspace")]
    private Guid WorkspaceId { get; set; }
    
    [Required]
    private WorkspaceEntity WorkspaceEntity { get; set; }

    public void UpdateInfo(string name, string? description, string color)
    {
        Name = name;
        Description = description;
        Color = color;
    }
}