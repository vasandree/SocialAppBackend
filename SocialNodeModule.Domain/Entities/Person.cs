namespace SocialNodeModule.Domain.Entities;

public class PersonEntity : BaseSocialNode
{
    private PersonEntity() { }
    
    public PersonEntity(Guid id, string name, string? description, string? avatarUrl, Guid userId, string? email, string? phoneNumber) : 
        base(id, name, description, avatarUrl, userId)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }

    private string? Email { get; set; }

    private string? PhoneNumber { get; set; }
    
    public void EditInfo(string name, string? description, string? avatar, string? email, string? phoneNumber)
    {
        base.EditInfo(name, description, avatar);
        Email = email;
        PhoneNumber = phoneNumber;
    }
}