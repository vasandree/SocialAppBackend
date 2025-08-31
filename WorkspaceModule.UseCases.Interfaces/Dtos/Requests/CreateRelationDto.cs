namespace WorkspaceModule.UseCases.Interfaces.Dtos.Requests;

public record CreateRelationDto(
    string Name,
    string? Description,
    string Color,
    Guid FirstUnit,
    Guid SecondUnit,
    Guid WorkspaceId);