using System.ComponentModel.DataAnnotations;
using User.Domain.Enums;

namespace User.Contracts.Dtos.Responses;

public class UserDto
{
    [Required] public required Guid Id { get; init; }

    [Required] public required long TelegramId { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    [Required] public required string UserName { get; init; }

    public string? PhotoUrl { get; init; }
    
}