using AutoMapper;
using Event.Contracts.Dtos.Responses;
using Event.Domain.Entities;

namespace Event.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EventTypeEntity, EventTypeResponseDto>();
    }
}