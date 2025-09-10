namespace UserModule.UseCases.Interfaces.Dtos.Responses;

public record UserDto
{
    public Guid Id { get; init; }
    public long TelegramId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string UserName { get; init; }
    public string? PhotoUrl { get; init; }
};