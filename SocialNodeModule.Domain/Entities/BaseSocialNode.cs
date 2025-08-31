using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNodeModule.Domain.Entities;

public class BaseSocialNode : CreatableEntity
{
    protected BaseSocialNode() { }
    
    protected BaseSocialNode(Guid id, string name, string? description, string? avatarUrl, Guid userId )
    {
        Id = id;
        Name = name;
        Description = description;
        AvatarUrl = avatarUrl;
        CreatorId = userId;
    }
    
    [Required] public string Name { get; set; }

    public string? Description { get; set; }

    public string? AvatarUrl { get; set; }
    
    public void EditInfo(string name, string? description, string? avatar)
    {
        Name = name;
        Description = description;
        AvatarUrl = avatar;
    }
}