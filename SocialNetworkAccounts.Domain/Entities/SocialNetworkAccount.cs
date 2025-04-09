using System.ComponentModel.DataAnnotations;
using Common;
using SocialNetworkAccounts.Domain.Enums;

namespace SocialNetworkAccounts.Domain.Entities;

public class SocialNetworkAccount: BaseEntity
{
    [Required]
    public string Username {get; set;}
    
    [Required]
    [EnumDataType(typeof(SocialNetwork))]
    public SocialNetwork Type {get; set;}
}