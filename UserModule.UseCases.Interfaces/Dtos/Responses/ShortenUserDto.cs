namespace UserModule.UseCases.Interfaces.Dtos.Responses;

public record ShortenUserDto
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string UserName { get; init; }
    public string? PhotoUrl { get; init; }
}