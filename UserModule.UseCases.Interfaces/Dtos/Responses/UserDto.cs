namespace UserModule.UseCases.Interfaces.Dtos.Responses;

public record UserDto(Guid Id, long TelegramId, string? FirstName, string? LastName, string UserName, string? PhotoUrl);