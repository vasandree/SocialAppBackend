using System.ComponentModel.DataAnnotations;

namespace UserService.Domain;

public class RefreshToken
{
    [Required]
    public string Token { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public DateTime Expires { get; set; }
}