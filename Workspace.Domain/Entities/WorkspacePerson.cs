using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain;

namespace Workspace.Domain.Entities;

public class WorkspacePerson : BaseEntity
{
    private WorkspacePerson() { }
    public WorkspacePerson(WorkspaceEntity workspaceEntity, Guid personId)
    {
        WorkspaceEntity = workspaceEntity;
        WorkspaceEntityId = workspaceEntity.Id;
        PersonId = personId;
    }

    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public Guid WorkspaceEntityId { get; set; }
    
    [ForeignKey("WorkspaceEntityId")]
    public WorkspaceEntity WorkspaceEntity { get; set; }

    [Required]
    public Guid PersonId { get; set; }
}
