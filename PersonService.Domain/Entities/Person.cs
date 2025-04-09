namespace PersonService.Domain.Entities;

public class Person : BaseSocialNode
{
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}