using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace Event.Domain.Entities;

public class EventType : BaseEntity
{
    [Required]
    public Guid CreatorId { get; set; }

    [Required]
    public string Name { get; set; }
    
}