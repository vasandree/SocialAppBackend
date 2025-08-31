using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace EventModule.Domain.Entities;

public class EventTypeEntity : CreatableEntity
{
    private EventTypeEntity() { }
    
    public EventTypeEntity(string name, Guid creatorId)
    {
        Name = name;
        CreatorId = creatorId;
    }
    
    [Required]  public string Name { get; private set; }
    
    public void UpdateName(string name) => Name = name;
}