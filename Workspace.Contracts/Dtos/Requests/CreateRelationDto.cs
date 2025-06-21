using System.ComponentModel.DataAnnotations;


namespace Workspace.Contracts.Dtos.Requests;

public class CreateRelationDto
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string Color { get; set; }
    
    [Required]
    public Guid FirstUnit { get; set; }
    
    [Required]
    public Guid SecondUnit { get; set; }
    
    public Guid WorkspaceId { get; set; }
}