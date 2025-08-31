namespace SocialNodeModule.Domain.Entities;

public class ClusterOfPeople : BaseSocialNode
{
    private ClusterOfPeople() { }
    
    public ClusterOfPeople(Guid id, string name, string? description, string? avatarUrl, Guid userId) 
        : base(id, name, description, avatarUrl, userId)
    {
    }

    private ICollection<PersonEntity> Persons { get; set; } = new List<PersonEntity>();
}