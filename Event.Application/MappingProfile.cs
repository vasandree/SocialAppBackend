using AutoMapper;
using Event.Contracts.Dtos.Responses;
using Event.Domain.Entities;

namespace Event.Application;

public class MappingProfile : Profile
{
    MappingProfile()
    {
        CreateMap<EventTypeEntity, EventTypeResponseDto>();
    }
}