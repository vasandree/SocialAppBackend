using AutoMapper;
using TaskModule.UseCases.Interfaces.Dtos.Responses;
using TaskModule.Domain.Entites;

namespace TaskModule.UseCases.Implementation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskEntity, TaskDto>();
        CreateMap<TaskEntity, ListedTaskDto>();
    }
}