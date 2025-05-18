using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain;

namespace Workspace.Domain.Entities;

public class Relation : BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Color { get; set; }
    
    [Required]
    public WorkspaceUnit FirstUnit { get; set; }
    
    [Required]
    public WorkspaceUnit SecondUnit { get; set; }
    
    [Required]
    [ForeignKey("Workspace")]
    public Guid WorkspaceId { get; set; }
    
    [Required]
    public WorkspaceEntity WorkspaceEntity { get; set; }
}