using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos.Responses;

public class TokensDto
{
    [Required]
    public string AccessToken { get; set; }
    
    [Required]
    public string RefreshToken { get; set; }
}