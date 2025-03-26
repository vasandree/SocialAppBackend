using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAccounts.Domain.Entities;

public class UsersAccount : SocialNetworkAccount
{
    [Required]
    public Guid UserId { get; set; }
}