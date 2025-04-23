using System.ComponentModel.DataAnnotations;

namespace Workspace.Domain.Entities;

public abstract class WorkspaceUnit
{
    [Key] 
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public string? Description { get; set; }
    
    [Required]
    public string Name { get; set; }
}