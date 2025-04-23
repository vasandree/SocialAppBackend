using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAccounts.Contracts.Dtos.Requests;

public class EditSocialNetworkAccountDto
{
    [Required] public string Username { get; set; }
}