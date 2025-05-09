using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace Event.Domain.Entities;

public class EventTypeEntity : BaseEntity
{


    [Required]
    public string Name { get; set; }
    
    [Required]
    public Guid CreatorId{ get; set; }
}