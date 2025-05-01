using System.ComponentModel.DataAnnotations;

namespace TaskModule.Contracts.Dtos.Responses;

public class TaskDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required] 
    public TaskStatus Status { get; set; } = TaskStatus.Created;

    [Required]
    public Guid SocialNodeId { get; set; }
}