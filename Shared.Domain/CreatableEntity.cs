using System.ComponentModel.DataAnnotations;

namespace Shared.Domain;

public abstract class CreatableEntity : BaseEntity
{
    [Required] 
    public Guid CreatorId { get; protected init; }
    
    public bool IsUserCreator(Guid userId) => CreatorId == userId;
}