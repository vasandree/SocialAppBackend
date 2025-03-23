using System.ComponentModel.DataAnnotations;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Requests;

public class AddSocialNetworkAccountDto
{
    [Required]
    public SocialNetwork Type { get; set; }
    
    [Required]
    public string Url { get; set; }
    
    [Required]
    public string Username { get; set; }
}