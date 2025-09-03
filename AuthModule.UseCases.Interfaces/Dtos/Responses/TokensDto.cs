using System.ComponentModel.DataAnnotations;

namespace AuthModule.UseCases.Interfaces.Dtos.Responses;

public record TokensDto(
    [Required] string AccessToken, 
    [Required] string RefreshToken);
