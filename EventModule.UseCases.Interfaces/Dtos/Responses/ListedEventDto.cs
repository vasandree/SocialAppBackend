namespace EventModule.UseCases.Interfaces.Dtos.Responses;

public record ListedEventDto(
    Guid Id, 
    string Name, 
    string? Location, 
    EventTypeResponseDto? EventType, 
    DateTime Date);