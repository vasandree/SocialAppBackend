using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workspace.Domain.Entities;

public class WorkspacePerson(Workspace workspace, Person person) : WorkspaceUnit
{
    [Required]
    [ForeignKey("Workspace")]
    public Guid WorkspaceId { get; init; } = workspace.Id;
    
    [Required]
    public Guid PersonId { get; init; } = person.Id;
    
    [Required]
    public Person Person { get; init; } = person;
    
    [Required]
    public Workspace Workspace { get; init; } = workspace;
}