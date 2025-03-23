
using System.ComponentModel.DataAnnotations;
using SocialNetworkAccounts.Domain.Enums;

namespace SocialNetworkAccounts.Application.Dtos.Requests;

public class AddSocialNetworkAccountDto
{
    [Required]
    public SocialNetwork Type { get; set; }
    
    
    [Required]
    public string Username { get; set; }
}