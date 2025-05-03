using System.ComponentModel.DataAnnotations;

namespace Auth.Contracts.Responses;

public class TokensDto
{
    [Required] public required string AccessToken { get; init; }

    [Required] public required string RefreshToken { get; init; }
}