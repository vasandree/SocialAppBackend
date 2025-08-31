namespace SocialNodeModule.Domain.Entities;

public class Place : BaseSocialNode
{
    private Place() { }
    
    public Place(Guid id, string name, string? description, string? avatarUrl, Guid userId) 
        : base(id, name, description, avatarUrl, userId)
    {
    }
}