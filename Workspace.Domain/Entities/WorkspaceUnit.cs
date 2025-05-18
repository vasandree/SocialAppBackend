using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain;

namespace Workspace.Domain.Entities;

public class WorkspaceUnit : BaseEntity
{
    public string? Description { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public Guid WorkspaceEntityId { get; set; }
    
    [ForeignKey("WorkspaceEntityId")]
    public WorkspaceEntity WorkspaceEntity { get; set; } 
}