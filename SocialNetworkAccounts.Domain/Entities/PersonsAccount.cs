using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAccounts.Domain.Entities;

public class PersonsAccount : SocialNetworkAccount
{
    [Required] public Guid PersonsId { get; init; }

    [Required] public Guid CreatorId { get; init; }
}