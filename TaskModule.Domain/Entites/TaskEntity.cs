using System.ComponentModel.DataAnnotations;
using Shared.Domain;
using TaskModule.Domain.Enums;

namespace TaskModule.Domain.Entites;

public class TaskEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public DateTime CreateDate { get; init; } = DateTime.UtcNow;
    
    [Required]
    public DateTime EndDate { get; set; }

    [Required] 
    public StatusOfTask Status { get; set; } = StatusOfTask.Created;

    [Required]
    public Guid SocialNodeId { get; set; }
    
    [Required]
    public Guid CreatorId { get; set; }
}