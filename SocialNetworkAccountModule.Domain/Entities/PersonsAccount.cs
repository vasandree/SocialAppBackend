using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccountModule.Domain.Entities;

public class PersonsAccount : SocialNetworkAccount
{
    private PersonsAccount() { }
    
    public PersonsAccount(Guid userId, string username, SocialNetwork type, Guid personId)
    {
        CreatorId = userId;
        UpdateUsername(username);
        Type = type;
        PersonsId = personId;
    }

    [Required] public Guid PersonsId { get; init; }
}