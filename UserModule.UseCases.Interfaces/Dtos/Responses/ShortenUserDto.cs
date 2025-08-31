namespace UserModule.UseCases.Interfaces.Dtos.Responses;

public record ShortenUserDto(Guid Id, string? FirstName, string? LastName, string UserName, string? PhotoUrl);