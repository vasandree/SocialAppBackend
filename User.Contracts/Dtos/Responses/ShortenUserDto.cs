using System.ComponentModel.DataAnnotations;

namespace User.Contracts.Dtos.Responses;

public class ShortenUserDto
{
    [Required] public required Guid Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    [Required] public required string UserName { get; init; }

    public string? PhotoUrl { get; init; }
}