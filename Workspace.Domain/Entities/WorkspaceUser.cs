using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workspace.Domain.Enums;

namespace Workspace.Domain.Entities;

public class WorkspaceUser(Workspace workspace, Guid userId, WorkspaceRole role)
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    [ForeignKey("Workspace")]
    public Guid WorkspaceId { get; init; } = workspace.Id;
    
    [Required]
    public Guid UserId { get; init; } = userId;
    
    [Required]
    public WorkspaceRole Role { get; init; } = role;
}