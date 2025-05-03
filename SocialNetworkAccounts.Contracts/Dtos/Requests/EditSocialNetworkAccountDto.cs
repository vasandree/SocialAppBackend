using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAccounts.Contracts.Dtos.Requests;

public class EditSocialNetworkAccountDto
{
    [Required] public required string Username { get; init; }
}