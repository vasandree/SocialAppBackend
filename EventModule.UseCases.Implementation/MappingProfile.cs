using AutoMapper;
using EventModule.Domain.Entities;
using EventModule.UseCases.Interfaces.Dtos.Responses;

namespace EventModule.UseCases.Implementation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EventTypeEntity, EventTypeResponseDto>();
    }
}