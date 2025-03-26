using System.ComponentModel.DataAnnotations;
using SocialNetworkAccounts.Domain.Enums;

namespace SocialNetworkAccounts.Domain.Entities;

public class SocialNetworkAccount
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    public string Username {get; set;}
    
    [Required]
    [EnumDataType(typeof(SocialNetwork))]
    public SocialNetwork Type {get; set;}
}