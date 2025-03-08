using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

public class RefreshToken
{
    [Required]
    public string Token { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [Required]
    public DateTime Expires { get; set; }
    
    public User User { get; set; }
}