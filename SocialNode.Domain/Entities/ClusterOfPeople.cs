namespace SocialNode.Domain.Entities;

public class ClusterOfPeople : BaseSocialNode
{
    private ICollection<PersonEntity> Persons { get; set; } = new List<PersonEntity>();
}