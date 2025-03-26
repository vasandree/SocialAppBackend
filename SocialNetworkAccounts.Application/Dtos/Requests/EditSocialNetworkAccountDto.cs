using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAccounts.Application.Dtos.Requests;

public class EditSocialNetworkAccountDto
{
    [Required] public string Username { get; set; }
}