using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAccounts.Domain.Entities;

public class PersonsAccount : SocialNetworkAccount
{
    [Required] public Guid PersonsId { get; set; }

    [Required] public Guid CreatorId { get; set; }
}