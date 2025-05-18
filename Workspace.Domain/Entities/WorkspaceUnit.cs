using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workspace.Domain.Entities;

public class WorkspaceUnit
{
    [Key] 
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public string? Description { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public Guid WorkspaceEntityId { get; set; }
    
    [ForeignKey("WorkspaceEntityId")]
    public WorkspaceEntity WorkspaceEntity { get; set; } 
}