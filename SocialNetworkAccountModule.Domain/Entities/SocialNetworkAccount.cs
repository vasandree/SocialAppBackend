using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccountModule.Domain.Entities;

public class SocialNetworkAccount : CreatableEntity
{
    [Required] private string Username { get; set; }

    [Required]
    [EnumDataType(typeof(SocialNetwork))]
    public SocialNetwork Type { get; init; }
    
    public void UpdateUsername(string username) => Username = username;
}