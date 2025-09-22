using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccountModule.Domain.Entities;

public class SocialNetworkAccount : CreatableEntity
{
    [Required] public string Username { get; private set; }

    [Required]
    [EnumDataType(typeof(SocialNetwork))]
    public SocialNetwork Type { get; protected init; }
    
    public void UpdateUsername(string username) => Username = username;
}