using System.ComponentModel.DataAnnotations;

namespace Workspace.Contracts.Dtos.Requests;

public record EditRelationDto()
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string Color { get; set; }
}