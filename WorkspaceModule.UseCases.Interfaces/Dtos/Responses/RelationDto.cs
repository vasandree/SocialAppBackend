namespace WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

public record RelationDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? Color { get; init; }
};