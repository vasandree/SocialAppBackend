namespace EventModule.UseCases.Interfaces.Dtos.Responses;

public record EventTypeResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}