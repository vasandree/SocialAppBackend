using AutoMapper;
using Workspace.Contracts.Dtos.Requests;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Domain.Entities;

namespace Workspace.Application;

public class MappingProfile : Profile
{
    MappingProfile()
    {
        CreateMap<WorkspaceEntity, ShortenWorkspaceDto>();
        CreateMap<WorkspaceEntity, ListedWorkspaceDto>();
        CreateMap<WorkspaceEntity, WorkspaceResponseDto>();
    }
}