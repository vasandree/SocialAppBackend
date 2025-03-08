using AutoMapper;
using UserService.Application.Dtos.Responses;
using UserService.Domain;

namespace UserService.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>();
    }
}