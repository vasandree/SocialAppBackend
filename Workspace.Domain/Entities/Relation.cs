using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain;

namespace Workspace.Domain.Entities;

public class RelationEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Color { get; set; }
    
    [Required]
    public Guid FirstSocialNode { get; set; }
    
    [Required]
    public Guid SecondSocialNode { get; set; }
    
    [Required]
    [ForeignKey("Workspace")]
    public Guid WorkspaceId { get; set; }
    
    [Required]
    public WorkspaceEntity WorkspaceEntity { get; set; }
    
    [Required]
    public Guid CreatorId { get; set; }
}