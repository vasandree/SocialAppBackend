using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entites;

public class RefreshToken
{
    [Key] [Required] public required string Token { get; init; }

    [Required] [ForeignKey("User")] public Guid UserId { get; init; }

    [Required] public DateTime Expires { get; init; }
}