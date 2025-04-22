using System.ComponentModel.DataAnnotations;

namespace Auth.Contracts.Responses;

public class TokensDto
{
    [Required]
    public string AccessToken { get; set; }
    
    [Required]
    public string RefreshToken { get; set; }
}