using System.ComponentModel.DataAnnotations;

namespace TaskModule.Contracts.Dtos.Requests;

public abstract record CreateTaskDto
{
    [Required]
    public string Name { get; init; }
    
    public string? Description { get; init; }
    
    [Required]
    public DateTime StartDate { get; init; }
    
    [Required]
    public DateTime EndDate { get; init; }
    
    [Required]
    public Guid SocialNodeId {get; init; }
}