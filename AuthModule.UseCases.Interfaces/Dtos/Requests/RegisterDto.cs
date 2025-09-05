using System.ComponentModel.DataAnnotations;

namespace AuthModule.UseCases.Interfaces.Dtos.Requests;

public record RegisterDto(
    [Required] string Username,
    [Required] string Password,
    [EmailAddress] string Email,
    string? FirstName,
    string? LastName);