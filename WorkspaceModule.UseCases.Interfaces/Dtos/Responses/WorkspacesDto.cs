using Shared.Contracts.Dtos;

namespace WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

public record WorkspacesDto(IReadOnlyList<ListedWorkspaceDto> Workspaces, Pagination Pagination);