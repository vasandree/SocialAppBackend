using Event.Contracts.Dtos.Responses;
using TaskModule.Contracts.Dtos.Responses;

namespace Workspace.Contracts.Dtos.Responses;

public record WorkspaceResponseDto
{
    public WorkspaceInfoDto Info { get; set; }
    public TasksDto Tasks { get; set; }
    public IReadOnlyList<ListedEventDto> Events { get; set; }
}