using System.ComponentModel.DataAnnotations;
using User.Contracts.Dtos;

namespace Auth.Contracts.Responses;

public class AuthResponse
{
    [Required]
    public TokensDto Tokens { get; set; }
    
    [Required]
    public UserSettingsDto Settings { get; set; }
}