using System.ComponentModel.DataAnnotations;

namespace PersonService.Domain;

public class ClusterOfPeople : BaseSocialNode
{
    private ICollection<Person> Persons { get; set; } = new List<Person>();
}