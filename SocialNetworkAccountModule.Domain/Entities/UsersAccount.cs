using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccountModule.Domain.Entities;

public class UsersAccount : SocialNetworkAccount
{
    private UsersAccount() { }
    
    public UsersAccount(Guid userId, string username, SocialNetwork type)
    {
        CreatorId = userId;
        UpdateUsername(username);
        Type = type;
    }

    [Required]
    public Guid UserId { get; init; }
    
    public bool CheckIfUserIdEquals(Guid userId) => UserId == userId;
}