using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccounts.Domain.Entities;

public class SocialNetworkAccount : BaseEntity
{
    [Required] public required string Username { get; set; }

    [Required]
    [EnumDataType(typeof(SocialNetwork))]
    public SocialNetwork Type { get; init; }
}