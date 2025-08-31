using System.ComponentModel.DataAnnotations;

namespace AuthModule.Domain.Entites;

public class RefreshToken
{
    [Key]
    [Required]
    public string Token { get; private set; } = null!;

    [Required] private Guid UserId { get;  }

    [Required] public DateTime Expires { get; init; }

    private RefreshToken() { }

    public RefreshToken(Guid userId, string token, DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        Expires = expiresAt;
    }

    public bool CheckExpired() => Expires <= DateTime.UtcNow;
    public bool IsOwnedByUser(Guid userId) => UserId == userId;
}