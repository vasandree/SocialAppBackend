using AutoMapper;
using TaskModule.Contracts.Dtos.Responses;
using TaskModule.Domain.Entites;

namespace TaskModule.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskEntity, TaskDto>();
        CreateMap<TaskEntity, ListedTaskDto>();
    }
}