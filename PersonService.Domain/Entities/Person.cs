using System.ComponentModel.DataAnnotations;

namespace PersonService.Domain;

public class Person : BaseSocialNode
{
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}