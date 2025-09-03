using System.ComponentModel.DataAnnotations;

namespace AuthModule.UseCases.Interfaces.Dtos.Requests;

public record CommonLoginDto(
    [Required] string Login,
    [Required] string Password);