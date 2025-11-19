using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace EventModule.Domain.Entities;

public class EventEntity : CreatableEntity
{
    private EventEntity() { }
    
    public EventEntity(List<Guid> socialNodeId, Guid workspaceId, DateTime date, string? description, string? location,
        string title)
    {
        SocialNodeId = socialNodeId;
        WorkspaceId = workspaceId;
        Date = date;
        Description = description;
        Location = location;
        Title = title;
    }

    [Required] public string Title { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public Guid? EventTypeId { get; private set; }
    public EventTypeEntity? EventType { get; private set; }
    
    [Required] public DateTime Date { get; set; }

    [Required] private List<Guid> SocialNodeId { get; }

    [Required] public Guid WorkspaceId { get; set; }
    
    public void AddEventType(EventTypeEntity eventType)
    {
        EventType = eventType;
        EventTypeId = eventType.Id;
    }

    public void RemoveEventType()
    {
        EventType = null;
        EventTypeId = null;
    }
    
    public void AddSocialNode(Guid socialNodeId) => SocialNodeId.Add(socialNodeId);
    
    public void RemoveSocialNode(Guid socialNodeId) => SocialNodeId.Remove(socialNodeId);
    
    public void UpdateInfo(DateTime date, string? description, string? location, string title)
    {
        Date = date;
        Description = description;
        Location = location;
        Title = title;
    }
    
    public bool CheckIfEventType(Guid? eventTypeId) => EventTypeId == eventTypeId;
    
    public IReadOnlyCollection<Guid> SocialNodeIds => SocialNodeId.AsReadOnly();
}