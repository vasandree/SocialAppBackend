using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entites;

public class RefreshToken
{
    [Key]
    [Required]
    public string Token { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [Required]
    public DateTime Expires { get; set; }
    
}