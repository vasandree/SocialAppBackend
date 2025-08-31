using AutoMapper;
using WorkspaceModule.UseCases.Interfaces.Dtos.Requests;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.Domain.Entities;

namespace WorkspaceModule.UseCases.Implementation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WorkspaceEntity, ShortenWorkspaceDto>();
        CreateMap<WorkspaceEntity, ListedWorkspaceDto>();
        CreateMap<WorkspaceEntity, WorkspaceResponseDto>();
        CreateMap<RelationEntity, RelationDto>();
    }
}