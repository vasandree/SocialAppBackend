namespace SocialNode.Domain.Entities;

public class PersonEntity : BaseSocialNode
{
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}