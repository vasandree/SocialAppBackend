namespace EventModule.UseCases.Interfaces.Dtos.Responses;

public record ListedEventDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Location { get; init; }
    public EventTypeResponseDto? EventType { get; init; }
    public DateTime Date { get; init; }
}