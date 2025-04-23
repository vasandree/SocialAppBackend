using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccounts.Contracts.Dtos.Responses;

public class SocialNetworkAccountDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public SocialNetwork Type { get; set; }
    
    [Required]
    public string Url { get; set; }
    
    [Required]
    public string Username { get; set; }
}