namespace PersonService.Domain.Entities;

public class ClusterOfPeople : BaseSocialNode
{
    private ICollection<Person> Persons { get; set; } = new List<Person>();
}