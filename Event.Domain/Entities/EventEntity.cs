using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace Event.Domain.Entities;

public class EventEntity : BaseEntity
{
    [Required]
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public string? Location { get; set; }
    
    public Guid? EventTypeId { get; set; }

    public EventTypeEntity? EventType { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public Guid SocialNodeId { get; set; }
}